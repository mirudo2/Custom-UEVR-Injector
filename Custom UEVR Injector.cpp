#include <windows.h>
#include <tlhelp32.h>
#include <psapi.h>
#include <iostream>
#include <string>
#include <tchar.h>

#pragma comment(lib, "psapi.lib")

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

std::wstring GetProcessDirectory(DWORD processID) {
    std::wstring processDirectory;
    HANDLE hProcess = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ, FALSE, processID);
    if (hProcess) {
        wchar_t processPath[MAX_PATH];
        if (GetModuleFileNameEx(hProcess, NULL, processPath, MAX_PATH)) {
            processDirectory = processPath;
            size_t pos = processDirectory.find_last_of(L"\\/");
            if (pos != std::wstring::npos) {
                processDirectory = processDirectory.substr(0, pos);
            }
        }
        CloseHandle(hProcess);
    }
    return processDirectory;
}

bool InjectDLL(DWORD processID, const std::wstring& dllPath) {
    HANDLE hProcess = OpenProcess(PROCESS_ALL_ACCESS, FALSE, processID);
    if (hProcess == NULL) {
        std::wcerr << L"Erro ao abrir o processo: " << GetLastError() << std::endl;
        return false;
    }

    LPVOID pDllPath = VirtualAllocEx(hProcess, NULL, (dllPath.size() + 1) * sizeof(wchar_t), MEM_COMMIT, PAGE_READWRITE);
    if (pDllPath == NULL) {
        std::wcerr << L"Erro ao alocar memória no processo: " << GetLastError() << std::endl;
        CloseHandle(hProcess);
        return false;
    }

    if (!WriteProcessMemory(hProcess, pDllPath, dllPath.c_str(), (dllPath.size() + 1) * sizeof(wchar_t), NULL)) {
        std::wcerr << L"Erro ao escrever na memória do processo: " << GetLastError() << std::endl;
        VirtualFreeEx(hProcess, pDllPath, 0, MEM_RELEASE);
        CloseHandle(hProcess);
        return false;
    }

    HMODULE hKernel32 = GetModuleHandle(L"kernel32.dll");
    if (hKernel32 == NULL) {
        std::wcerr << L"Erro ao obter handle do Kernel32: " << GetLastError() << std::endl;
        VirtualFreeEx(hProcess, pDllPath, 0, MEM_RELEASE);
        CloseHandle(hProcess);
        return false;
    }

    LPTHREAD_START_ROUTINE pLoadLibraryW = (LPTHREAD_START_ROUTINE)GetProcAddress(hKernel32, "LoadLibraryW");
    if (pLoadLibraryW == NULL) {
        std::wcerr << L"Erro ao obter endereço da LoadLibraryW: " << GetLastError() << std::endl;
        VirtualFreeEx(hProcess, pDllPath, 0, MEM_RELEASE);
        CloseHandle(hProcess);
        return false;
    }

    HANDLE hThread = CreateRemoteThread(hProcess, NULL, 0, pLoadLibraryW, pDllPath, 0, NULL);
    if (hThread == NULL) {
        std::wcerr << L"Erro ao criar thread remota: " << GetLastError() << std::endl;
        VirtualFreeEx(hProcess, pDllPath, 0, MEM_RELEASE);
        CloseHandle(hProcess);
        return false;
    }

    WaitForSingleObject(hThread, INFINITE);

    VirtualFreeEx(hProcess, pDllPath, 0, MEM_RELEASE);
    CloseHandle(hThread);
    CloseHandle(hProcess);

    return true;
}


int main() {
    std::wstring defaultProcessName = L"Client-Win64-Shipping.exe";
    std::wstring processName = defaultProcessName;  // Initialize with default value
    std::wstring dllToInject = L"UEVRBackend.dll";
    std::wstring dllToCopy = L"UEVRBackend.dll";
    std::wstring dllToCopy2 = L"openxr_loader.dll";

    std::wcout << L"Enter the process name (press Enter to use the default \"" << defaultProcessName << L"\"): ";
    std::getline(std::wcin, processName);

    // If the user input is empty, use the default process name
    if (processName.empty()) {
        processName = defaultProcessName;
    }


        DWORD processID = GetProcessIDByName(processName);
        if (processID != 0) {
            TerminateProcessById(processID);
            Sleep(2000);
        }

    while (true) {
        DWORD processID = GetProcessIDByName(processName);
        if (processID != 0) {
            std::wcout << L"Processo encontrado! ID: " << processID << std::endl;
            
            std::wstring processDirectory = GetProcessDirectory(processID);

            if (!processDirectory.empty()) {
                std::wcout << L"Diretorio do processo: " << processDirectory << std::endl;
                std::wstring destPath = processDirectory + L"\\" + dllToCopy;
                std::wstring destPath2 = processDirectory + L"\\" + dllToCopy2;

                if (CopyFile(dllToCopy.c_str(), destPath.c_str(), FALSE) and CopyFile(dllToCopy2.c_str(), destPath2.c_str(), FALSE)) {

                    std::wcout << L"Arquivo " << dllToCopy << L" copiado para " << destPath << std::endl;
                    std::wcout << L"Arquivo " << dllToCopy2 << L" copiado para " << destPath2 << std::endl;

                    if (InjectDLL(processID, dllToInject)) {
                        std::wcout << L"DLL " << dllToInject << L" injetada com sucesso!" << std::endl;
                    }
                    else {
                        std::wcerr << L"Falha ao injetar a DLL." << std::endl;
                    }
                }
                else {
                    std::wcerr << L"Erro ao copiar arquivo: " << GetLastError() << std::endl;
                }
            }
            else {
                std::wcerr << L"Falha ao obter o diretório do processo." << std::endl;
            }
            break;
        }
        else {
            std::wcout << L"Start the game now ..." << std::endl;
        }
    }
    Sleep(100000);

    return 0;
}
