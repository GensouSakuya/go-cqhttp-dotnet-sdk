﻿using System.Threading;
using System.Threading.Tasks;

namespace GensouSakuya.GoCqhttp.Sdk.Sessions
{
    public abstract partial class BaseSession
    {
        #region Static
        static BaseSession()
        {
            PrepareHandlerAutoMap();
        }
        #endregion

        protected bool UseSsl { get; private set; }
        protected string Host { get; private set; }
        protected int Port { get; private set; }
        protected string AccessToken { get; private set; }

        public BaseSession(string host,
                       int port,
                       string accessToken,
                       bool useSsl = false)
        {
            UseSsl = useSsl;
            Host = host;
            Port = port;
            AccessToken = accessToken;
        }

        public abstract Task ConnectAsync(CancellationToken cancellationToken = default);
    }
}
