using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace MvcDemo.Framework.Wcf.ServiceManager
{
    public abstract class ServiceLocator
    {
        private const string _clientSection = "system.serviceModel/client";
        private static Hashtable _contractsEndPoints = new Hashtable();

        static ServiceLocator()
        {
            ServiceLocator.GetEndpointsByConfig();
        }
        /// <summary>
        /// Método privado que cria o objeto do tipo Binding através 
        /// ChannelEndpointElement passado como parâmetro para a função
        /// </summary>
        /// <param name="sectionGroup"></param>
        /// <returns></returns>
        private static Binding GetBinding(ChannelEndpointElement endpoint)
        {
            Binding binding = null;

            string tipoEndpoint = endpoint.Binding.ToString().ToLower();

            switch (tipoEndpoint)
            {
                case "wshttpbinding":
                    binding = (Binding)new System.ServiceModel.WSHttpBinding();
                    break;
                case "basichttpbinding":
                    binding = (Binding)new System.ServiceModel.BasicHttpBinding();
                    ((BasicHttpBinding)binding).Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
                    ((BasicHttpBinding)binding).Security.Transport.ClientCredentialType = HttpClientCredentialType.Ntlm;
                    ((BasicHttpBinding)binding).MaxReceivedMessageSize = 500000000;// int.MaxValue;
                    ((BasicHttpBinding)binding).MaxBufferSize = 500000000;//int.MaxValue;

                    ((BasicHttpBinding)binding).OpenTimeout = new TimeSpan(0, 10, 0);
                    ((BasicHttpBinding)binding).CloseTimeout = new TimeSpan(0, 10, 0);
                    ((BasicHttpBinding)binding).SendTimeout = new TimeSpan(0, 10, 0);
                    /*((BasicHttpBinding)_binding).ReaderQuotas.MaxArrayLength = 4 * 1024 * 1024;
                    ((BasicHttpBinding)_binding).ReaderQuotas.MaxBytesPerRead = 4 * 1024 * 1024;
                    ((BasicHttpBinding)_binding).ReaderQuotas.MaxDepth = 2147483647;
                    ((BasicHttpBinding)_binding).ReaderQuotas.MaxStringContentLength = 2147483647;
                    ((BasicHttpBinding)_binding).ReaderQuotas.MaxNameTableCharCount = 2147483647;*/

                    break;
            }

            return binding;
        }

        /// <summary>
        /// Monta Properties do Request, e adiciona Cookies de autentificação no header HTTP
        /// </summary>
        /// <returns></returns>
        private static HttpRequestMessageProperty GetRequestProperties()
        {
            HttpRequestMessageProperty reqProp = new HttpRequestMessageProperty();
            reqProp.SuppressEntityBody = false;
            //reqProp.Headers.Add(HttpRequestHeader.Cookie, ConsoleManager.Cookie); 

            return reqProp;
        }

        /// <summary>
        /// Obtem todos os Endpoints do web.Config e adiciona no Hash
        /// </summary>
        private static void GetEndpointsByConfig()
        {
            ClientSection clientSection = (ClientSection)System.Web.Configuration.WebConfigurationManager.GetSection(_clientSection);

            foreach (ChannelEndpointElement endPoint in clientSection.Endpoints)
            {
                _contractsEndPoints.Add(endPoint.Contract, endPoint);
            }
        }

        /// <summary>
        /// Instancia serviço WCF de acordo com predicado informado
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetInstance<T>()
        {
            try
            {
                ChannelEndpointElement endPoint = (ChannelEndpointElement)_contractsEndPoints[typeof(T).UnderlyingSystemType.FullName];
                if (endPoint == null)
                {
                    return default(T);
                }
                else
                {
                    EndpointAddress epa = new EndpointAddress(endPoint.Address.AbsoluteUri);

                    //Cria canal pelo ChannelFactory
                    Binding _binding = ServiceLocator.GetBinding(endPoint);

                    ChannelFactory<T> anotherChannelFactory = new ChannelFactory<T>(_binding);
                    //anotherChannelFactory.Endpoint.Behaviors.Add(new ExceptionMarshallingBehavior());
                    anotherChannelFactory.Open();

                    foreach (OperationDescription op in anotherChannelFactory.Endpoint.Contract.Operations)
                    {
                        DataContractSerializerOperationBehavior dataContractBehavior = op.Behaviors[typeof(DataContractSerializerOperationBehavior)] as DataContractSerializerOperationBehavior;
                        if (dataContractBehavior != null)
                        {
                            dataContractBehavior.MaxItemsInObjectGraph = int.MaxValue;
                        }
                    }

                    T typedProxy = anotherChannelFactory.CreateChannel(epa);

                    //destroy
                    anotherChannelFactory = null;

                    return typedProxy;
                }
            }
            catch (System.Exception err)
            {
                //Lança erro de criação do serviço no WCF
                throw new InvalidOperationException("Erro na criação do Serviço via ServiceLocator: " + err.Message);
            }
            return default(T);
        }

    }
}
