namespace AsyncProgramming
{
	partial class CalcWindow {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.btnTask = new System.Windows.Forms.Button();
            this.btnAwaitAsync = new System.Windows.Forms.Button();
            this.btnThread = new System.Windows.Forms.Button();
            this.btnSynchronous = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.grid = new System.Windows.Forms.TableLayoutPanel();
            this.grid.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnTask
            // 
            this.btnTask.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTask.Location = new System.Drawing.Point(10, 247);
            this.btnTask.Margin = new System.Windows.Forms.Padding(10, 10, 10, 10);
            this.btnTask.Name = "btnTask";
            this.btnTask.Size = new System.Drawing.Size(450, 63);
            this.btnTask.TabIndex = 0;
            this.btnTask.Text = "Task";
            this.btnTask.UseVisualStyleBackColor = true;
            this.btnTask.Click += new System.EventHandler(this.TaskButtonHandler);
            // 
            // btnAwaitAsync
            // 
            this.btnAwaitAsync.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAwaitAsync.Location = new System.Drawing.Point(10, 330);
            this.btnAwaitAsync.Margin = new System.Windows.Forms.Padding(10, 10, 10, 10);
            this.btnAwaitAsync.Name = "btnAwaitAsync";
            this.btnAwaitAsync.Size = new System.Drawing.Size(450, 64);
            this.btnAwaitAsync.TabIndex = 2;
            this.btnAwaitAsync.Text = "Await/Async";
            this.btnAwaitAsync.UseVisualStyleBackColor = true;
            this.btnAwaitAsync.Click += new System.EventHandler(this.AsyncAwaitButtonHandler);
            // 
            // btnThread
            // 
            this.btnThread.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThread.Location = new System.Drawing.Point(10, 164);
            this.btnThread.Margin = new System.Windows.Forms.Padding(10, 10, 10, 10);
            this.btnThread.Name = "btnThread";
            this.btnThread.Size = new System.Drawing.Size(450, 63);
            this.btnThread.TabIndex = 3;
            this.btnThread.Text = "Thread";
            this.btnThread.UseVisualStyleBackColor = true;
            this.btnThread.Click += new System.EventHandler(this.ThreadButtonHandler);
            // 
            // btnSynchronous
            // 
            this.btnSynchronous.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSynchronous.Location = new System.Drawing.Point(10, 81);
            this.btnSynchronous.Margin = new System.Windows.Forms.Padding(10, 10, 10, 10);
            this.btnSynchronous.Name = "btnSynchronous";
            this.btnSynchronous.Size = new System.Drawing.Size(450, 63);
            this.btnSynchronous.TabIndex = 4;
            this.btnSynchronous.Text = "Synchronous";
            this.btnSynchronous.UseVisualStyleBackColor = true;
            this.btnSynchronous.Click += new System.EventHandler(this.SynchronousButtonHandler);
            // 
            // txtResult
            // 
            this.txtResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResult.Location = new System.Drawing.Point(10, 10);
            this.txtResult.Margin = new System.Windows.Forms.Padding(10, 10, 10, 10);
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.Size = new System.Drawing.Size(450, 51);
            this.txtResult.TabIndex = 5;
            this.txtResult.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // grid
            // 
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.ColumnCount = 1;
            this.grid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.grid.Controls.Add(this.btnTask, 0, 3);
            this.grid.Controls.Add(this.btnAwaitAsync, 0, 4);
            this.grid.Controls.Add(this.txtResult, 0, 0);
            this.grid.Controls.Add(this.btnSynchronous, 0, 1);
            this.grid.Controls.Add(this.btnThread, 0, 2);
            this.grid.Location = new System.Drawing.Point(24, 23);
            this.grid.Margin = new System.Windows.Forms.Padding(10, 10, 10, 10);
            this.grid.Name = "grid";
            this.grid.RowCount = 5;
            this.grid.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.grid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.grid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.grid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.grid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.grid.Size = new System.Drawing.Size(470, 404);
            this.grid.TabIndex = 6;
            // 
            // CalcWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 454);
            this.Controls.Add(this.grid);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.MinimumSize = new System.Drawing.Size(474, 415);
            this.Name = "CalcWindow";
            this.Text = "Async Programming";
            this.Load += new System.EventHandler(this.CalcWindow_Load);
            this.grid.ResumeLayout(false);
            this.grid.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnTask;
		private System.Windows.Forms.Button btnAwaitAsync;
		private System.Windows.Forms.Button btnThread;
		private System.Windows.Forms.Button btnSynchronous;
		private System.Windows.Forms.TextBox txtResult;
		private System.Windows.Forms.TableLayoutPanel grid;
	}
}

