﻿using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using log4net;
using System.Reflection;
using Bussiness;
using SqlDataProvider.Data;

namespace Tank.Request
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class SpiritInfoList : IHttpHandler
    {

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Write(Bulid(context));

        }

        public static string Bulid(HttpContext context)
        {
            bool value = false;
            string message = "Fail";

            XElement result = new XElement("Result");

            try
            {
                using (ProduceBussiness db = new ProduceBussiness())
                {
                    SpiritInfo[] infos = db.GetAllSpiritInfo();
                    foreach (SpiritInfo info in infos)
                    {
                        result.Add(Road.Flash.FlashUtils.CreateSpiritInfo(info));
                    }
                }

                value = true;
                message = "Success!";
            }
            catch (Exception ex)
            {
                log.Error("SpiritInfoList", ex);
            }

            result.Add(new XAttribute("value", value));
            result.Add(new XAttribute("message", message));
            //return result.ToString(false);
            return csFunction.CreateCompressXml(context, result, "SpiritInfoList", true);

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
