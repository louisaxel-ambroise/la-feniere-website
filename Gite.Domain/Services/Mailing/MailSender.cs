using System;
using Gite.Model.Model;

namespace Gite.Model.Services.Mailing
{
    public class MailSender : IMailSender
    {
        private readonly string _from;
        private readonly string _password;

        public MailSender(string from, string password)
        {
            if (@from == null) throw new ArgumentNullException("from");
            if (password == null) throw new ArgumentNullException("password");

            _from = @from;
            _password = password;
        }

        public void Send(string address, Mail mail)
        {
            throw new System.NotImplementedException();
        }
    }
}