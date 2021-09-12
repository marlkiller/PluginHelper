using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using PluginHelper.Entity;
using PluginHelper.Handler;
using PluginHelper.Native;
using PluginHelper.Service;

namespace PluginHelper
{
  public partial class Form1 : Form
  {
    private PluginHelperService pluginHelperService = new();

    
    public Form1()
    {
      InitializeComponent();
      
      // #if DEBUG
      NativeMethods.AllocConsole();
      ConsoleHelper.WriteLine("注意：启动程序...");
      ConsoleHelper.WriteLine(string.Format("DateTime  : {0}",new DateTime()));
      ConsoleHelper.WriteLine("\tWritten by {0}","vvvvvvoid");
      ConsoleHelper.WriteLine(Level.DEBUG, "这是一条{0}信息","DEBUG");
      ConsoleHelper.WriteLine(Level.INFO, $"{"这是一条INFO信息"}");
      ConsoleHelper.WriteLine(Level.WAIN, $"{"这是一条WARN信息"}");
      ConsoleHelper.WriteLine(Level.ERROR, $"{"这是一条ERROR信息"}");
      // #endif  
    }
  

    private void Form1_Load(object sender, EventArgs e)
    {
      btnRefresh_Click(sender, e);
    }

    private void btnRefresh_Click(object sender, EventArgs e)
    {

      comboBoxProcessList.Items.Clear();
      string filterName = null;
      if (checkBoxFlag.Checked)
      {
        filterName = textProcessNameFilter.Text.Trim();
      }
      List<ProcessEntity>  processEntities = pluginHelperService.refreshProcessList(filterName);
      
      foreach (var processEntity in processEntities)
      {
        comboBoxProcessList.Items.Add($"{processEntity.ProcessId}|{processEntity.ProcessFullPath}");
      }
    }

    private void textModulesView_TextChanged(object sender, EventArgs e)

    {
      textModulesView.SelectionStart = textModulesView.Text.Length;
      textModulesView.ScrollToCaret(); 
    }
    
    private void comboBoxProcessList_SelectedIndexChanged(object sender, EventArgs e)
    {
      var selectedValue = comboBoxProcessList.SelectedItem;
      if (selectedValue!=null)
      {
        textModulesView.Text = "";
        if (!string.IsNullOrEmpty(selectedValue.ToString()))
        {
          var strings = selectedValue.ToString().Split('|');
          if (strings.Length==2)
          {
            textProcessNameFilter.Text = strings[1];
            var processId = Convert.ToInt32(strings[0]);
            if (processId>=0)
            {
              pluginHelperService.processId = new IntPtr(processId);
              var modules = pluginHelperService.getModulesWithProcessId(processId);
              foreach (ProcessModule module in modules)
              {
                textModulesView.Text += String.Format("{0}\r\nBaseAddress:{1}\tSize:{2}\r\n",module.FileName,module.BaseAddress.ToString("x").ToUpper(),module.ModuleMemorySize.ToString("x").ToUpper());
              }
            }
          }
        }
      }
    }

    private void btnOpenFile_Click(object sender, EventArgs e)
    {
      OpenFileDialog dialog = new OpenFileDialog();
      dialog.Filter = "DLL动态链接库(*.dll)|*.dll";
      var dialogResult = dialog.ShowDialog(this);
      textDllPath.Text = dialog.FileName;
    }

    private void btnInject_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(textDllPath.Text))
      {
        return;
      }
      Boolean flag = pluginHelperService.dllInject(textDllPath.Text);
      if (flag)
      {
        comboBoxProcessList_SelectedIndexChanged(sender, e);
      }
    }

    private void btnUninject_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(textDllPath.Text))
      {
        return;
      }
      Boolean flag = pluginHelperService.dllUnInject(textDllPath.Text);
      if (flag)
      {
        comboBoxProcessList_SelectedIndexChanged(sender, e);
      }
    }

    private void btnInjectCode_Click(object sender, EventArgs e)
    {
      
      if (string.IsNullOrEmpty(textAsmCode.Text))
      {
        return;
      }

      Boolean flag = pluginHelperService.codeInject(textAsmCode.Text);
    }

    private void btnMsgInject_Click(object sender, EventArgs e)
    {
     Boolean flag = pluginHelperService.msgInject(textBoxMsgTitle.Text, textBoxMsgValue.Text);
    }
    

    private void button1_Click(object sender, EventArgs e)
    {
    }

    private void btnGdi_Click(object sender, EventArgs e)
    {
      pluginHelperService.drawGDI(200,300,400,500);
    }

    private void btnGdiPlu_Click(object sender, EventArgs e)
    {
      pluginHelperService.drawGDIPlus(200,300,400,500);
    }
    GameEspForm gameEspForm ;

    private void btnNetGdi_Click(object sender, EventArgs e)
    {
      if (gameEspForm == null)
      {
        gameEspForm = new GameEspForm();
        gameEspForm.prosessId = pluginHelperService.processId;
      }

      if (!gameEspForm.thread.IsAlive)
      {
        gameEspForm.thread.Start(new object());
      }
      else
      {
        gameEspForm.thread.Abort();
      }
    }

  }
}
