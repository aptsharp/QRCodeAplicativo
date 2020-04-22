using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.IO;

namespace QRCodeAplicativo.Controllers
{
    public class QRCoderController : Controller
    {
        public IActionResult Index()
            //Interface de codigo chamando o index()
        {
            return View();
            // retorna um view ou seja uma pagina view.
        }

        [HttpPost]
        // token para fazer um posto no codigo
        public IActionResult Index(string qrTexto)
            // puxando de uma interface e passando como parametro um texto
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            // estanciando/criando um novo objeto QRCodeGenerator
            QRCodeData qRCodeData = qrGenerator.CreateQrCode(qrTexto, QRCodeGenerator.ECCLevel.Q);
            // manda para a biblioteca a string
            QRCode qRCode = new QRCode(qRCodeData);
            // estancia um novo objeto QRCode passando como pareametro o qRCodeData
            Bitmap qrCodeImage = qRCode.GetGraphic(10);
            // cria a imagem QR e usa a biblioteca Graphic para escolher o tamanho da imagem 
            return View(BitmapToBytes(qrCodeImage));
            // retorna a image QRCode na para a view que ira exibir na tela. 
        }

        private static Byte[] BitmapToBytes(Bitmap img)
        // classe privada apenas para uso do BitmapToBytes // sem esta calsse o metodo BitmapToBytes da erro esperando algum retorno
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                // puxa a formulação da imagem depois que ela é gerada
                return stream.ToArray();
                // retorna a imagem
            }
        }
    }
}