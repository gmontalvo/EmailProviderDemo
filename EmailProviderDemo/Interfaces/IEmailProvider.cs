using System.Collections.Generic;

namespace EmailProviderDemo
{
    interface IEmailProvider
    {
        string From { get; set; }

        void AddTo(string email);
        void AddTo(List<string> emails);

        string Subject { get; set; }
        string Body { get; set; }
    }
}
