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

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (EmailProviderFactory.ProviderType provider in EmailProviderFactory.GetProviders())
            {
                IEmailProvider email = EmailProviderFactory.Get(provider);

                // Add the message properties.
                email.From = "john@example.com";

                // Add multiple addresses to the To field.
                List<String> recipients = new List<String>
                {
                    @"Jeff Smith <jeff@example.com>",
                    @"Anna Lidman <anna@example.com>",
                    @"Peter Saddow <peter@example.com>",
                };

                email.AddTo(recipients);
                email.Subject = string.Format("Testing the {0} Provider", email.GetType().Name);

                //Add the HTML and Text bodies
                email.Body = "<p>Hello World!</p>";
            }

        }
    }
}
