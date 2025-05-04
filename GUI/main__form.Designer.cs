
namespace Custom_UEVR_Injector
{
    partial class main__form
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main__form));
            this.button_game_folder = new System.Windows.Forms.Button();
            this.groupBoxMethod = new System.Windows.Forms.GroupBox();
            this.radioButtonAlt = new System.Windows.Forms.RadioButton();
            this.radioButtonSync = new System.Windows.Forms.RadioButton();
            this.radioButtonNative = new System.Windows.Forms.RadioButton();
            this.checkBoxAutoInject = new System.Windows.Forms.CheckBox();
            this.checkBoxNullify = new System.Windows.Forms.CheckBox();
            this.groupBoxRuntime = new System.Windows.Forms.GroupBox();
            this.radioButtonVR = new System.Windows.Forms.RadioButton();
            this.radioButtonXR = new System.Windows.Forms.RadioButton();
            this.checkBoxFPS = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listResults = new System.Windows.Forms.TextBox();
            this.button_uevr_folder = new System.Windows.Forms.Button();
            this.button_profile_folder = new System.Windows.Forms.Button();
            this.button_inject = new System.Windows.Forms.Button();
            this.button_shortcut = new System.Windows.Forms.Button();
            this.checkBoxFocusGame = new System.Windows.Forms.CheckBox();
            this.checkBox_auto_close = new System.Windows.Forms.CheckBox();
            this.groupBoxMethod.SuspendLayout();
            this.groupBoxRuntime.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_game_folder
            // 
            this.button_game_folder.BackColor = System.Drawing.Color.DarkSlateGray;
            this.button_game_folder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_game_folder.FlatAppearance.BorderSize = 0;
            this.button_game_folder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_game_folder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_game_folder.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button_game_folder.Location = new System.Drawing.Point(12, 351);
            this.button_game_folder.Name = "button_game_folder";
            this.button_game_folder.Size = new System.Drawing.Size(509, 47);
            this.button_game_folder.TabIndex = 1;
            this.button_game_folder.Text = "Select the game folder";
            this.button_game_folder.UseVisualStyleBackColor = false;
            this.button_game_folder.Click += new System.EventHandler(this.button_game_folder_Click);
            // 
            // groupBoxMethod
            // 
            this.groupBoxMethod.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxMethod.Controls.Add(this.radioButtonAlt);
            this.groupBoxMethod.Controls.Add(this.radioButtonSync);
            this.groupBoxMethod.Controls.Add(this.radioButtonNative);
            this.groupBoxMethod.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxMethod.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBoxMethod.Location = new System.Drawing.Point(15, 179);
            this.groupBoxMethod.Name = "groupBoxMethod";
            this.groupBoxMethod.Size = new System.Drawing.Size(196, 108);
            this.groupBoxMethod.TabIndex = 3;
            this.groupBoxMethod.TabStop = false;
            this.groupBoxMethod.Text = "Rendering Method";
            // 
            // radioButtonAlt
            // 
            this.radioButtonAlt.AutoSize = true;
            this.radioButtonAlt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonAlt.Location = new System.Drawing.Point(6, 69);
            this.radioButtonAlt.Name = "radioButtonAlt";
            this.radioButtonAlt.Size = new System.Drawing.Size(109, 19);
            this.radioButtonAlt.TabIndex = 2;
            this.radioButtonAlt.Text = "Alternating/AFR";
            this.radioButtonAlt.UseVisualStyleBackColor = true;
            this.radioButtonAlt.CheckedChanged += new System.EventHandler(this.radioButtonAlt_CheckedChanged);
            // 
            // radioButtonSync
            // 
            this.radioButtonSync.AutoSize = true;
            this.radioButtonSync.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonSync.Location = new System.Drawing.Point(6, 46);
            this.radioButtonSync.Name = "radioButtonSync";
            this.radioButtonSync.Size = new System.Drawing.Size(161, 19);
            this.radioButtonSync.TabIndex = 1;
            this.radioButtonSync.Text = "Synchronized Sequential";
            this.radioButtonSync.UseVisualStyleBackColor = true;
            this.radioButtonSync.CheckedChanged += new System.EventHandler(this.radioButtonSync_CheckedChanged);
            // 
            // radioButtonNative
            // 
            this.radioButtonNative.AutoSize = true;
            this.radioButtonNative.Checked = true;
            this.radioButtonNative.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonNative.Location = new System.Drawing.Point(6, 23);
            this.radioButtonNative.Name = "radioButtonNative";
            this.radioButtonNative.Size = new System.Drawing.Size(98, 19);
            this.radioButtonNative.TabIndex = 0;
            this.radioButtonNative.TabStop = true;
            this.radioButtonNative.Text = "Native Stereo";
            this.radioButtonNative.UseVisualStyleBackColor = true;
            this.radioButtonNative.CheckedChanged += new System.EventHandler(this.radioButtonNative_CheckedChanged);
            // 
            // checkBoxAutoInject
            // 
            this.checkBoxAutoInject.AutoSize = true;
            this.checkBoxAutoInject.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxAutoInject.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxAutoInject.Location = new System.Drawing.Point(115, 577);
            this.checkBoxAutoInject.Name = "checkBoxAutoInject";
            this.checkBoxAutoInject.Size = new System.Drawing.Size(103, 24);
            this.checkBoxAutoInject.TabIndex = 4;
            this.checkBoxAutoInject.Text = "Auto inject";
            this.checkBoxAutoInject.UseVisualStyleBackColor = false;
            this.checkBoxAutoInject.CheckedChanged += new System.EventHandler(this.checkBoxAutoInject_CheckedChanged);
            // 
            // checkBoxNullify
            // 
            this.checkBoxNullify.AutoSize = true;
            this.checkBoxNullify.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxNullify.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxNullify.Location = new System.Drawing.Point(16, 121);
            this.checkBoxNullify.Name = "checkBoxNullify";
            this.checkBoxNullify.Size = new System.Drawing.Size(122, 19);
            this.checkBoxNullify.TabIndex = 5;
            this.checkBoxNullify.Text = "Nullify VR Plugins";
            this.checkBoxNullify.UseVisualStyleBackColor = false;
            this.checkBoxNullify.CheckedChanged += new System.EventHandler(this.checkBoxNullify_CheckedChanged);
            // 
            // groupBoxRuntime
            // 
            this.groupBoxRuntime.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxRuntime.Controls.Add(this.radioButtonVR);
            this.groupBoxRuntime.Controls.Add(this.radioButtonXR);
            this.groupBoxRuntime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxRuntime.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBoxRuntime.Location = new System.Drawing.Point(15, 20);
            this.groupBoxRuntime.Name = "groupBoxRuntime";
            this.groupBoxRuntime.Size = new System.Drawing.Size(116, 83);
            this.groupBoxRuntime.TabIndex = 6;
            this.groupBoxRuntime.TabStop = false;
            this.groupBoxRuntime.Text = "Runtime";
            // 
            // radioButtonVR
            // 
            this.radioButtonVR.AutoSize = true;
            this.radioButtonVR.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonVR.Location = new System.Drawing.Point(8, 46);
            this.radioButtonVR.Name = "radioButtonVR";
            this.radioButtonVR.Size = new System.Drawing.Size(71, 19);
            this.radioButtonVR.TabIndex = 1;
            this.radioButtonVR.Text = "OpenVR";
            this.radioButtonVR.UseVisualStyleBackColor = true;
            this.radioButtonVR.CheckedChanged += new System.EventHandler(this.radioButtonVR_CheckedChanged);
            // 
            // radioButtonXR
            // 
            this.radioButtonXR.AutoSize = true;
            this.radioButtonXR.Checked = true;
            this.radioButtonXR.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonXR.Location = new System.Drawing.Point(8, 23);
            this.radioButtonXR.Name = "radioButtonXR";
            this.radioButtonXR.Size = new System.Drawing.Size(72, 19);
            this.radioButtonXR.TabIndex = 0;
            this.radioButtonXR.TabStop = true;
            this.radioButtonXR.Text = "OpenXR";
            this.radioButtonXR.UseVisualStyleBackColor = true;
            this.radioButtonXR.CheckedChanged += new System.EventHandler(this.radioButtonXR_CheckedChanged);
            // 
            // checkBoxFPS
            // 
            this.checkBoxFPS.AutoSize = true;
            this.checkBoxFPS.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxFPS.Checked = true;
            this.checkBoxFPS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxFPS.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxFPS.Location = new System.Drawing.Point(16, 144);
            this.checkBoxFPS.Name = "checkBoxFPS";
            this.checkBoxFPS.Size = new System.Drawing.Size(83, 19);
            this.checkBoxFPS.TabIndex = 7;
            this.checkBoxFPS.Text = "Show FPS";
            this.checkBoxFPS.UseVisualStyleBackColor = false;
            this.checkBoxFPS.CheckedChanged += new System.EventHandler(this.checkBoxFPS_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(537, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "Performance";
            // 
            // listResults
            // 
            this.listResults.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.listResults.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listResults.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.listResults.Location = new System.Drawing.Point(224, 23);
            this.listResults.Multiline = true;
            this.listResults.Name = "listResults";
            this.listResults.ReadOnly = true;
            this.listResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.listResults.Size = new System.Drawing.Size(297, 301);
            this.listResults.TabIndex = 9;
            // 
            // button_uevr_folder
            // 
            this.button_uevr_folder.BackColor = System.Drawing.Color.DarkSlateGray;
            this.button_uevr_folder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_uevr_folder.FlatAppearance.BorderSize = 0;
            this.button_uevr_folder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_uevr_folder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_uevr_folder.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button_uevr_folder.Location = new System.Drawing.Point(12, 404);
            this.button_uevr_folder.Name = "button_uevr_folder";
            this.button_uevr_folder.Size = new System.Drawing.Size(509, 47);
            this.button_uevr_folder.TabIndex = 1;
            this.button_uevr_folder.Text = "Select the UEVR folder";
            this.button_uevr_folder.UseVisualStyleBackColor = false;
            this.button_uevr_folder.Click += new System.EventHandler(this.button_uevr_folder_Click);
            // 
            // button_profile_folder
            // 
            this.button_profile_folder.BackColor = System.Drawing.Color.DarkSlateGray;
            this.button_profile_folder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_profile_folder.Enabled = false;
            this.button_profile_folder.FlatAppearance.BorderSize = 0;
            this.button_profile_folder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_profile_folder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_profile_folder.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button_profile_folder.Location = new System.Drawing.Point(962, 618);
            this.button_profile_folder.Name = "button_profile_folder";
            this.button_profile_folder.Size = new System.Drawing.Size(183, 46);
            this.button_profile_folder.TabIndex = 1;
            this.button_profile_folder.Text = "Profile folder";
            this.button_profile_folder.UseVisualStyleBackColor = false;
            this.button_profile_folder.Click += new System.EventHandler(this.button_profile_folder_Click);
            // 
            // button_inject
            // 
            this.button_inject.BackColor = System.Drawing.Color.Firebrick;
            this.button_inject.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_inject.Enabled = false;
            this.button_inject.FlatAppearance.BorderSize = 0;
            this.button_inject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_inject.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_inject.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button_inject.Location = new System.Drawing.Point(15, 515);
            this.button_inject.Name = "button_inject";
            this.button_inject.Size = new System.Drawing.Size(509, 52);
            this.button_inject.TabIndex = 1;
            this.button_inject.Text = "Select the game folder and the UEVR folder";
            this.button_inject.UseVisualStyleBackColor = false;
            this.button_inject.Click += new System.EventHandler(this.button_inject_Click);
            // 
            // button_shortcut
            // 
            this.button_shortcut.BackColor = System.Drawing.Color.DarkSlateGray;
            this.button_shortcut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_shortcut.Enabled = false;
            this.button_shortcut.FlatAppearance.BorderSize = 0;
            this.button_shortcut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_shortcut.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_shortcut.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button_shortcut.Location = new System.Drawing.Point(773, 618);
            this.button_shortcut.Name = "button_shortcut";
            this.button_shortcut.Size = new System.Drawing.Size(183, 46);
            this.button_shortcut.TabIndex = 1;
            this.button_shortcut.Text = "Create a shortcut";
            this.button_shortcut.UseVisualStyleBackColor = false;
            this.button_shortcut.Click += new System.EventHandler(this.button_shortcut_Click);
            // 
            // checkBoxFocusGame
            // 
            this.checkBoxFocusGame.AutoSize = true;
            this.checkBoxFocusGame.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxFocusGame.Checked = true;
            this.checkBoxFocusGame.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxFocusGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxFocusGame.Location = new System.Drawing.Point(224, 578);
            this.checkBoxFocusGame.Name = "checkBoxFocusGame";
            this.checkBoxFocusGame.Size = new System.Drawing.Size(200, 24);
            this.checkBoxFocusGame.TabIndex = 10;
            this.checkBoxFocusGame.Text = "Focus game on injection";
            this.checkBoxFocusGame.UseVisualStyleBackColor = false;
            this.checkBoxFocusGame.CheckedChanged += new System.EventHandler(this.checkBoxFocusGame_CheckedChanged);
            // 
            // checkBox_auto_close
            // 
            this.checkBox_auto_close.AutoSize = true;
            this.checkBox_auto_close.BackColor = System.Drawing.Color.Transparent;
            this.checkBox_auto_close.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_auto_close.Location = new System.Drawing.Point(116, 608);
            this.checkBox_auto_close.Name = "checkBox_auto_close";
            this.checkBox_auto_close.Size = new System.Drawing.Size(222, 24);
            this.checkBox_auto_close.TabIndex = 11;
            this.checkBox_auto_close.Text = "Close injector after injecting";
            this.checkBox_auto_close.UseVisualStyleBackColor = false;
            this.checkBox_auto_close.CheckedChanged += new System.EventHandler(this.checkBox_auto_close_CheckedChanged);
            // 
            // main__form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSlateGray;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1162, 685);
            this.Controls.Add(this.checkBox_auto_close);
            this.Controls.Add(this.checkBoxFocusGame);
            this.Controls.Add(this.listResults);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxFPS);
            this.Controls.Add(this.groupBoxRuntime);
            this.Controls.Add(this.checkBoxNullify);
            this.Controls.Add(this.checkBoxAutoInject);
            this.Controls.Add(this.groupBoxMethod);
            this.Controls.Add(this.button_inject);
            this.Controls.Add(this.button_shortcut);
            this.Controls.Add(this.button_profile_folder);
            this.Controls.Add(this.button_uevr_folder);
            this.Controls.Add(this.button_game_folder);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "main__form";
            this.Text = "Custom UEVR Injector - by Polar";
            this.groupBoxMethod.ResumeLayout(false);
            this.groupBoxMethod.PerformLayout();
            this.groupBoxRuntime.ResumeLayout(false);
            this.groupBoxRuntime.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.GroupBox groupBoxMethod;
        public System.Windows.Forms.RadioButton radioButtonAlt;
        public System.Windows.Forms.RadioButton radioButtonSync;
        public System.Windows.Forms.RadioButton radioButtonNative;
        public System.Windows.Forms.CheckBox checkBoxAutoInject;
        public System.Windows.Forms.CheckBox checkBoxNullify;
        public System.Windows.Forms.GroupBox groupBoxRuntime;
        public System.Windows.Forms.RadioButton radioButtonVR;
        public System.Windows.Forms.RadioButton radioButtonXR;
        public System.Windows.Forms.CheckBox checkBoxFPS;
        public System.Windows.Forms.Button button_game_folder;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox listResults;
        public System.Windows.Forms.Button button_uevr_folder;
        public System.Windows.Forms.Button button_profile_folder;
        public System.Windows.Forms.Button button_inject;
        public System.Windows.Forms.Button button_shortcut;
        public System.Windows.Forms.CheckBox checkBoxFocusGame;
        public System.Windows.Forms.CheckBox checkBox_auto_close;
    }
}

