using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace YGOM
{
	internal partial class MainWindow : Window
	{
		public MainWindow()
		{
			this.InitializeComponent();
		}

		private void ButtonSend_Click(object sender, RoutedEventArgs e)
		{
			byte[] data = NetworkMain.Entry(this.TextBoxText.Text, null, this.TextBoxToken.Text, int.Parse(this.TextBoxId.Text));
			WebClient webClient = new WebClient();
			webClient.Headers.Set("User-Agent", "UnityPlayer/5.6.1p1 (UnityWebRequest/1.0, libcurl/7.51.0-DEV)");
			webClient.Headers.Set("Accept", "*/*");
			webClient.Headers.Set("Accept-Encoding", "identity");
			webClient.Headers.Set("Content-Type", "application/octet-stream");
			webClient.Headers.Set("X-Unity-Version", "5.6.1p1");
			webClient.Headers.Set("x_acts", this.TextBoxText.Text);
			try
			{
				this.TextBoxResponse.Text = webClient.Encoding.GetString(webClient.UploadData("https://att-s-jpb.mo.konami.net/att/api", data));
			}
			catch (Exception ex)
			{
				this.TextBoxResponse.Text = ex.ToString();
			}
		}
	}
}
