using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmailProviderDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (EmailProviderFactory.ProviderType provider in EmailProviderFactory.GetProviders())
            {
                IEmailProvider email = EmailProviderFactory.Get(provider);
                comboBox1.Items.Add(email.GetType().Name);
            }

            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
        }
    }
}
