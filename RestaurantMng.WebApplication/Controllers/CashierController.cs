using Newtonsoft.Json;
using RestaurantMng.Service.Interfaces;
using RestaurantMng.WebApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text.RegularExpressions;
using System.Text;
using Document = iTextSharp.text.Document;
using System.Web;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using RestaurantMng.Core.Common;
using RestaurantMng.WebApplication.Authorization;

namespace RestaurantMng.WebApplication.Controllers
{
    [Authorization(Role=SystemRole.Thungan)]
    public class CashierController : Controller
    {
        private readonly ICashierService _iOrderService;
        public CashierController(ICashierService iOrderService)
        {
            _iOrderService = iOrderService;
        }
        // GET: Manage

        public ActionResult Index()
        {
            var res = _iOrderService.GetAllOrder();
            var json = JsonConvert.SerializeObject(res);
            List<OrderOrderItemVM> resVM = new List<OrderOrderItemVM>();
            if (json != "{}" && json != "[]")
            {
                resVM = JsonConvert.DeserializeObject<List<OrderOrderItemVM>>(json);
            }

            return View(resVM);
        }

        [HttpGet]
        public ActionResult Spending()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Summary(DateTime? fromDate, DateTime? toDate, int type)
        {
            var res = _iOrderService.ThongKe(fromDate, toDate, type);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DetailOrder(int? tableId, int? orderId)
        {
            if (orderId == null)
                return PartialView("PVDetailOrder", null);
            var res = _iOrderService.GetDetailOrder(orderId.Value);

            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult TinhTien(int orderId)
        {
            string name = null;
            var res = _iOrderService.ThanhToan(orderId, out name);
            if (!res)
                return new HttpStatusCodeResult(500, "Internal server error!");

          //  return RedirectToAction("Index_Post", "Cashier", new { orderId = orderId});

            return Json(new { message = "Thanh toán thành công", orderId = orderId, tableName = name }, JsonRequestBehavior.AllowGet);
        }

        #region PDF
        public static string convertToUnSign3(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        [HttpPost]
        public ActionResult Index_Post(int orderId, string tableName)
        {
           var resDetail = _iOrderService.GetDetailOrder(orderId);

            
            var json = JsonConvert.SerializeObject(resDetail);
            var resVM = JsonConvert.DeserializeObject<List<OrderItemDetailVM>>(json);



            string sylfaenpath = Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\sylfaen.ttf";
            BaseFont sylfaen = BaseFont.CreateFont(sylfaenpath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font head = new Font(sylfaen, 12f, Font.NORMAL, BaseColor.BLUE);

            Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 15);
            iTextSharp.text.pdf.PdfWriter pdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();

            string sylfaenpath1 = Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\sylfaen.ttf";
            BaseFont sylfaen1 = BaseFont.CreateFont(sylfaenpath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font head1 = new Font(sylfaen, 12f, Font.NORMAL, BaseColor.BLUE);
            Font normal1 = new Font(sylfaen, 10f, Font.NORMAL, BaseColor.BLACK);
            Font underline1 = new Font(sylfaen, 10f, Font.UNDERLINE, BaseColor.BLACK);

            //Top Heading
            Chunk chunk = new Chunk(convertToUnSign3("Bill"), new Font(sylfaen, 20, Font.BOLDITALIC, BaseColor.MAGENTA));
            pdfDoc.Add(chunk);

            //Horizontal Line
            Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            pdfDoc.Add(line);

            //Table
            PdfPTable table = new PdfPTable(2);
            table.WidthPercentage = 100;
            //0=Left, 1=Centre, 2=Right
            table.HorizontalAlignment = 0;
            table.SpacingBefore = 20f;
            table.SpacingAfter = 30f;

            //Cell no 1
            PdfPCell cell = new PdfPCell();
            cell.Border = 0;

            chunk = new Chunk(convertToUnSign3(tableName), new Font(sylfaen, 20, Font.NORMAL, BaseColor.RED));


            cell.AddElement(chunk);
            table.AddCell(cell);

            //Cell no 2
            chunk = new Chunk(convertToUnSign3("Restaurant DĐP\nAddress: 7 District, Hồ Chí Minh City"), new Font(sylfaen, 15, Font.NORMAL, BaseColor.PINK));
            cell = new PdfPCell();
            cell.Border = 0;
            cell.AddElement(chunk);
            table.AddCell(cell);

            //Add table to document
            pdfDoc.Add(table);

            //Horizontal Line
            line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            pdfDoc.Add(line);

            //Table
            table = new PdfPTable(5);
            table.WidthPercentage = 100;
            table.HorizontalAlignment = 0;
            table.SpacingBefore = 20f;
            table.SpacingAfter = 30f;

            //Cell
            cell = new PdfPCell();
            chunk = new Chunk(convertToUnSign3("Order infomation"));
            cell.AddElement(chunk);
            cell.Colspan = 5;
            cell.BackgroundColor = BaseColor.PINK;
            table.AddCell(cell);

            table.AddCell("STT");
            table.AddCell(convertToUnSign3("Name"));
            table.AddCell(convertToUnSign3("Note"));
            table.AddCell(convertToUnSign3("Quantity"));
            table.AddCell(convertToUnSign3("Price"));
            decimal totalPrice = 0;
            decimal surcharge = 0;
            foreach (var item in resVM)
            {
                table.AddCell($"{resVM.IndexOf(item)+1}");
                table.AddCell(item.ItemName);
                table.AddCell(item.Note);
                table.AddCell($"{item.Quantity}");
                table.AddCell($"{item.Price} vnd");
                totalPrice += item.Price * item.Quantity;
                surcharge = item.Surcharge;
            }

            pdfDoc.Add(table);

            Paragraph para = new Paragraph();
            para.Add(convertToUnSign3($"Surcharge: {surcharge} vnđ\n\n"));
            para.Add(convertToUnSign3($"Total price: {totalPrice + surcharge} vnđ\n\nThank you!"));
            pdfDoc.Add(para);

            //Horizontal Line
            line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            pdfDoc.Add(line);

            para = new Paragraph();
            para.Add(convertToUnSign3($"Date: {DateTime.Now.ToShortTimeString()}"));
            para.SpacingBefore = 20f;
            para.SpacingAfter = 20f;
            pdfDoc.Add(para);

            ////Creating link
            //chunk = new Chunk("How to Create a Pdf File");
            //chunk.Font = new Font(sylfaen, 25, Font.BOLD, BaseColor.RED);
            //chunk.SetAnchor("http://www.yogihosting.com/create-pdf-asp-net-mvc/");
            //pdfDoc.Add(chunk);

            pdfWriter.CloseStream = false;
            pdfDoc.Close();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
         //   Response.AddHeader("Content-Type", "application/octet-stream");
            Response.AddHeader("content-disposition", $"attachment;filename=Bill_{orderId}.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Write(pdfDoc);
            Response.End();


            return View();
        }
        #endregion PDF
        [HttpGet]
        public ActionResult GetAll()
        {
            return View();
        }
    }
}