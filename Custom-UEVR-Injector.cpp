#include <windows.h>
#include <tlhelp32.h>
#include <iostream>
#include <vector>
#include <string>
#include <filesystem>
#include <fstream>
#include <shlobj.h> // Para criação de atalhos
#include <shobjidl.h> // Para IFileDialog
#include <comdef.h>
#include <atlbase.h>
#include <atlcom.h>
#include <codecvt>
#include <io.h>
#include <fcntl.h>

// Certifique-se de que estamos usando o namespace std::filesystem
namespace fs = std::filesystem;

void LogMessage(const std::wstring& message);
std::vector<DWORD> GetProcessIds(const std::wstring& processName);
bool InjectDLL(DWORD processId, const std::wstring& dllPath);
bool CreateShortcut(const std::wstring& shortcutPath, const std::wstring& targetPath, const std::wstring& arguments);
std::pair<std::wstring, std::wstring> SelectFolderAndFileName(const std::wstring& defaultName);
std::wstring trim(const std::wstring& str);
DWORD GetProcessIDByName(const std::wstring& processName);

// Função para finalizar processo pelo PID
bool TerminateProcessById(DWORD processId) {
    HANDLE hProcess = OpenProcess(PROCESS_TERMINATE, FALSE, processId);
    if (hProcess == NULL) {
        return false;
    }
    BOOL result = TerminateProcess(hProcess, 0);
    CloseHandle(hProcess);
    return result == TRUE;
}

std::wstring convertToWstring(const char* input) {
    // Determinar o tamanho necessário para a conversão usando a codificação local
    int size_needed = MultiByteToWideChar(CP_ACP, 0, input, -1, nullptr, 0);

    if (size_needed == 0) {
        throw std::runtime_error("Erro ao determinar o tamanho necessário para a conversão.");
    }

    // Alocar memória para a string wide
    std::wstring wstr(size_needed - 1, 0); // '-1' para descartar o terminador '\0'

    // Converter de char para wchar_t
    MultiByteToWideChar(CP_ACP, 0, input, -1, &wstr[0], size_needed);

    return wstr;
}

int main(int argc, char* argv[]) {

    _setmode(_fileno(stdout), _O_U16TEXT);
    _setmode(_fileno(stdin), _O_U16TEXT);

    // Obtém o handle para o console
    HANDLE hConsole = GetStdHandle(STD_OUTPUT_HANDLE);

    // Salva o atributo original para restaurar depois
    CONSOLE_SCREEN_BUFFER_INFO consoleInfo;
    GetConsoleScreenBufferInfo(hConsole, &consoleInfo);
    WORD originalColor = consoleInfo.wAttributes;

    std::wcout << L"Custom UEVR Injector by Polar." << std::endl;
    SetConsoleTextAttribute(hConsole, FOREGROUND_GREEN | FOREGROUND_INTENSITY);
    std::wcout << L"Note: To ensure this works, launch SteamVR or the Oculus app before running this Injector. For some games, the injection will only succeed if this step is performed first.\n" << std::endl;
    SetConsoleTextAttribute(hConsole, originalColor);

    LogMessage(L"Custom UEVR Injector started.");

    wchar_t console_path[MAX_PATH]; // Buffer para armazenar o caminho do executável
    DWORD size = GetModuleFileNameW(NULL, console_path, MAX_PATH);

    // Definir o diretório de trabalho para o mesmo local do executável
    fs::path exeDir = fs::path(console_path).parent_path();
    fs::current_path(exeDir);

    std::wcout << L"Current working directory set to: " << fs::current_path() << std::endl;

    // Obter o caminho completo do arquivo
    fs::path logPath = fs::current_path() / L"Custom-UEVR-Injector-Log.txt";

    // Verificar se o arquivo existe
    if (fs::exists(logPath)) {
        fs::remove(logPath);
    }

    std::wstring processName;
    std::wstring is_openvr;
    std::wstring nullify_plugins;
    int is_args;

    if (argc > 3) {
        processName = trim(convertToWstring(argv[1]));
        is_openvr = trim(convertToWstring(argv[2]));
        nullify_plugins = trim(convertToWstring(argv[3]));

        is_args = true;
    }
    else {
        std::wcout << L"Enter the process name: ";
        std::getline(std::wcin, processName);

        std::wcout << L"Choose an API, OpenXR(0) or OpenVR(1): ";
        std::getline(std::wcin, is_openvr);

        std::wcout << L"Nullify VR Plugins? (0/1): ";
        std::getline(std::wcin, nullify_plugins);

        is_args = false;
    }

    std::wcout << L"Looking at the process:: " + processName + L"\n";

    DWORD processID = GetProcessIDByName(processName);
    if (processID != 0) {
        TerminateProcessById(processID);
        Sleep(2000);
    }

    // Permitir que o usuário escolha a pasta e o nome do atalho
    std::wstring shortcutDir;
    std::wstring shortcutName;
    if (!is_args) {
        auto folderAndFileName = SelectFolderAndFileName(processName);
        shortcutDir = folderAndFileName.first;
        shortcutName = folderAndFileName.second;

        if (shortcutDir.empty() || shortcutName.empty()) {
            std::wcerr << L"No folder or file name selected. Exiting." << std::endl;
            LogMessage(L"No folder or file name selected for shortcut. Exiting.");
            return 1;
        }
    }

    std::wstring shortcutPath = shortcutDir + L"\\" + shortcutName + L".lnk";

    // Create shortcut
    if (!is_args) {

        std::wstring exePath(console_path);

        if (!CreateShortcut(shortcutPath, L"\"" + exePath + L"\"", L"\"" + processName + L"\" " + is_openvr + L" " + nullify_plugins)) {
            std::wcerr << L"Failed to create shortcut." << std::endl;
            LogMessage(L"Failed to create shortcut for: " + processName);
            return 1;
        }

        std::wcout << L"Shortcut created at: " << shortcutPath << std::endl;
        LogMessage(L"Shortcut created at: " + shortcutPath);
    }

    std::vector<std::wstring> dlls = {};

    if (nullify_plugins == L"1") {
        dlls.push_back(L"UEVRPluginNullifier.dll");
    }

    if (is_openvr == L"0") {
        dlls.push_back(L"openxr_loader.dll");
    }

    if (is_openvr == L"1") {
        dlls.push_back(L"openvr_api.dll");
    }

    if (is_openvr != L"0" && is_openvr != L"1") {
        LogMessage(L"Choose the correct runtime 0 or 1.");
        return 1;
    }

    dlls.push_back(L"LuaVR.dll");
    dlls.push_back(L"UEVRBackend.dll");

    std::wcout << L"Start your game now..." << std::endl;
    LogMessage(L"Waiting for the process to start: " + processName);

    DWORD processId;

    while (true) {
        std::vector<DWORD> processIds = GetProcessIds(processName);

        if (!processIds.empty()) {
            for (DWORD processId : processIds) {

                for (const std::wstring& dll : dlls) {
                    std::wstring dllPath = fs::current_path().wstring() + L"\\" + dll;

                    // Verificar se a DLL existe
                    if (fs::exists(dllPath)) {
                        if (InjectDLL(processId, dllPath)) {
                            std::wcout << L"Successfully injected " << dll << L" into process " << processName << std::endl;
                            //LogMessage(L"Successfully injected " + dll + L" into process " + processName);
                        }
                        else {
                            std::wcerr << L"Failed to inject " << dll << L" into process " << processName << std::endl;
                            LogMessage(L"Failed to inject " + dll + L" into process " + processName);
                        }
                    }
                    else {
                        std::wcerr << L"Skipping injection, DLL not found: " << dllPath << std::endl;
                        LogMessage(L"Skipping injection, DLL not found: " + dllPath);
                    }
                }

            }
            break;  // Saia do loop depois de injetar as DLLs
        }

        Sleep(10);
    }

    LogMessage(L"Injection process completed.");
    return 0;
}

void LogMessage(const std::wstring& message) {

    wchar_t console_path[MAX_PATH]; // Buffer para armazenar o caminho do executável
    DWORD size = GetModuleFileNameW(NULL, console_path, MAX_PATH);

    // Definir o diretório de trabalho para o mesmo local do executável
    fs::path exeDir = fs::path(console_path).parent_path();
    fs::current_path(exeDir);

    fs::path logPath = fs::current_path() / L"Custom-UEVR-Injector-Log.txt";
    std::wstring wLogPath = logPath.wstring();

    std::wofstream logFile(wLogPath, std::ios::app);
    if (logFile.is_open()) {
        SYSTEMTIME time;
        GetLocalTime(&time);
        logFile << L"[" << time.wYear << L"-" << time.wMonth << L"-" << time.wDay << L" "
            << time.wHour << L":" << time.wMinute << L":" << time.wSecond << L"] "
            << message << std::endl;
        logFile.close();
    }
}

std::vector<DWORD> GetProcessIds(const std::wstring& processName) {
    std::vector<DWORD> processIds;
    PROCESSENTRY32 processEntry;
    processEntry.dwSize = sizeof(PROCESSENTRY32);

    HANDLE snapshot = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);
    if (snapshot == INVALID_HANDLE_VALUE) {
        LogMessage(L"Failed to create process snapshot.");
        return processIds;
    }

    if (Process32First(snapshot, &processEntry)) {
        do {
            if (processName == processEntry.szExeFile) {
                processIds.push_back(processEntry.th32ProcessID);
            }
        } while (Process32Next(snapshot, &processEntry));
    }

    CloseHandle(snapshot);
    return processIds;
}

bool InjectDLL(DWORD processId, const std::wstring& dllPath) {
    HANDLE process = OpenProcess(PROCESS_ALL_ACCESS, FALSE, processId);
    if (!process) {
        LogMessage(L"Failed to open process: " + std::to_wstring(processId));
        return false;
    }

    LPVOID allocMem = VirtualAllocEx(process, nullptr, dllPath.size() * sizeof(wchar_t), MEM_RESERVE | MEM_COMMIT, PAGE_READWRITE);
    if (!allocMem) {
        CloseHandle(process);
        LogMessage(L"Failed to allocate memory in process: " + std::to_wstring(processId));
        return false;
    }

    if (!WriteProcessMemory(process, allocMem, dllPath.c_str(), dllPath.size() * sizeof(wchar_t), nullptr)) {
        VirtualFreeEx(process, allocMem, 0, MEM_RELEASE);
        CloseHandle(process);
        LogMessage(L"Failed to write memory in process: " + std::to_wstring(processId));
        return false;
    }

    HANDLE thread = CreateRemoteThread(process, nullptr, 0, (LPTHREAD_START_ROUTINE)LoadLibraryW, allocMem, 0, nullptr);
    if (!thread) {
        VirtualFreeEx(process, allocMem, 0, MEM_RELEASE);
        CloseHandle(process);
        LogMessage(L"Failed to create remote thread in process: " + std::to_wstring(processId));
        return false;
    }

    WaitForSingleObject(thread, INFINITE);
    VirtualFreeEx(process, allocMem, 0, MEM_RELEASE);
    CloseHandle(thread);
    CloseHandle(process);

    LogMessage(L"DLL injected successfully: " + dllPath + L" into process " + std::to_wstring(processId));
    return true;
}

bool CreateShortcut(const std::wstring& shortcutPath, const std::wstring& targetPath, const std::wstring& arguments) {
    CoInitialize(nullptr);
    CComPtr<IShellLink> shellLink;
    HRESULT hr = shellLink.CoCreateInstance(CLSID_ShellLink, nullptr, CLSCTX_INPROC_SERVER);

    if (FAILED(hr)) {
        CoUninitialize();
        LogMessage(L"Failed to create IShellLink instance.");
        return false;
    }

    // Define o caminho do executável
    shellLink->SetPath(targetPath.c_str());

    // Define os argumentos (se fornecidos)
    if (!arguments.empty()) {
        shellLink->SetArguments(arguments.c_str());
    }

    CComPtr<IPersistFile> persistFile;
    hr = shellLink.QueryInterface(&persistFile);
    if (FAILED(hr)) {
        CoUninitialize();
        LogMessage(L"Failed to query IPersistFile interface.");
        return false;
    }

    // Salva o atalho no caminho especificado
    hr = persistFile->Save(shortcutPath.c_str(), TRUE);
    CoUninitialize();

    if (SUCCEEDED(hr)) {
        LogMessage(L"Shortcut successfully created at: " + shortcutPath);
    }
    else {
        LogMessage(L"Failed to save shortcut at: " + shortcutPath);
    }

    return SUCCEEDED(hr);
}

std::pair<std::wstring, std::wstring> SelectFolderAndFileName(const std::wstring& defaultName) {
    // Inicializar COM
    HRESULT hr = CoInitialize(nullptr);
    if (FAILED(hr)) {
        LogMessage(L"Failed to initialize COM.");
        return { L"", L"" };
    }

    CComPtr<IFileDialog> pFileDialog;
    hr = CoCreateInstance(CLSID_FileSaveDialog, nullptr, CLSCTX_INPROC_SERVER, IID_PPV_ARGS(&pFileDialog));

    if (FAILED(hr)) {
        LogMessage(L"Failed to create FileSaveDialog instance.");
        CoUninitialize();
        return { L"", L"" };
    }

    // Configurar o nome padrão com suporte a Unicode
    if (!defaultName.empty()) {
        hr = pFileDialog->SetFileName(defaultName.c_str());
        if (FAILED(hr)) {
            LogMessage(L"Failed to set default file name.");
        }
    }

    // Definir tipos de arquivos
    COMDLG_FILTERSPEC fileTypes[] = { {L"Shortcut", L"*.lnk"} };
    pFileDialog->SetFileTypes(1, fileTypes);

    // Exibir a caixa de diálogo
    hr = pFileDialog->Show(nullptr);
    if (FAILED(hr)) {
        LogMessage(L"Folder or file name selection canceled or failed.");
        CoUninitialize();
        return { L"", L"" };
    }

    // Obter o arquivo selecionado
    CComPtr<IShellItem> pItem;
    hr = pFileDialog->GetResult(&pItem);
    if (FAILED(hr)) {
        LogMessage(L"Failed to get selected folder or file name.");
        CoUninitialize();
        return { L"", L"" };
    }

    PWSTR pszFilePath = nullptr;
    hr = pItem->GetDisplayName(SIGDN_FILESYSPATH, &pszFilePath);
    if (FAILED(hr)) {
        LogMessage(L"Failed to retrieve file path.");
        CoUninitialize();
        return { L"", L"" };
    }

    // Converter para std::wstring e liberar memória
    std::wstring fullPath(pszFilePath);
    CoTaskMemFree(pszFilePath);
    CoUninitialize();

    // Separar diretório e nome do arquivo
    size_t lastSlash = fullPath.find_last_of(L"\\/");
    if (lastSlash != std::wstring::npos) {
        return { fullPath.substr(0, lastSlash), fullPath.substr(lastSlash + 1) };
    }

    return { L"", L"" };
}

std::wstring trim(const std::wstring& str) {
    const std::wstring whitespace = L" \t\n\r\f\v\u00A0";
    size_t first = str.find_first_not_of(whitespace);
    size_t last = str.find_last_not_of(whitespace);

    if (first == std::wstring::npos || last == std::wstring::npos) {
        return L""; // Retorna string vazia se não houver caracteres visíveis
    }

    std::wstring trimmed = str.substr(first, last - first + 1);

    // Remover explicitamente o caractere '\0' que pode estar no meio ou no final
    trimmed.erase(std::remove(trimmed.begin(), trimmed.end(), L'\0'), trimmed.end());

    return trimmed;
}

DWORD GetProcessIDByName(const std::wstring& processName) {
    DWORD processID = 0;
    HANDLE hSnapshot = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);
    if (hSnapshot != INVALID_HANDLE_VALUE) {
        PROCESSENTRY32 pe;
        pe.dwSize = sizeof(PROCESSENTRY32);
        if (Process32First(hSnapshot, &pe)) {
            do {
                if (processName == pe.szExeFile) {
                    processID = pe.th32ProcessID;
                    break;
                }
            } while (Process32Next(hSnapshot, &pe));
        }
        CloseHandle(hSnapshot);
    }
    return processID;
}
