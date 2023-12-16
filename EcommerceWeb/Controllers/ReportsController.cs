using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Microsoft.AspNetCore.Http;

namespace EcommerceWeb.Models
{
    public class ReportsController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReportsController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult StocksReport()
        {
            try
            {
                // Connect to SQL Server and retrieve data
                string connectionString = @"Data Source=.;Initial Catalog=R49_MyEcommerce_db;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand command = new SqlCommand(@"Select * from Products p inner join Categories c on c.CategoryID=p.CategoryID inner join Suppliers s on s.SupplierID=p.SupplierID", connection);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "Products");

                // Load and configure Crystal Report
                ReportDocument reportDocument = new ReportDocument();
                string reportPath = _httpContextAccessor.HttpContext.Request.PathBase;
                reportDocument.Load(reportPath);
                reportDocument.SetDataSource(dataSet);

                // Export report to PDF and return it as a file
                Stream stream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Flush();
                reportDocument.Close();
                reportDocument.Dispose();
                return File(stream, System.Net.Mime.MediaTypeNames.Application.Pdf);
            }
            catch (Exception ex)
            {
                return Content("<h2>Error:" + ex.Message + "</h2>", "text/html");
            }
        }

        public ActionResult CustomersReport()
        {
            try
            {
                // Connect to SQL Server and retrieve data
                string connectionString = @"Data Source=.;Initial Catalog=R49_MyEcommerce_db;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand command = new SqlCommand("Select * from Customers", connection);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "Customers");

                // Load and configure Crystal Report
                ReportDocument reportDocument = new ReportDocument();
                string reportPath = _httpContextAccessor.HttpContext.Request.PathBase;
                reportDocument.Load(reportPath);
                reportDocument.SetDataSource(dataSet);

                // Export report to PDF and return it as a file
                Stream stream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                reportDocument.Close();
                reportDocument.Dispose();
                return File(stream, System.Net.Mime.MediaTypeNames.Application.Pdf);
            }
            catch (Exception ex)
            {
                return Content("<h2>Error:" + ex.Message + "</h2>", "text/html");
            }
        }

        public ActionResult SalesReport()
        {
            try
            {
                // Connect to SQL Server and retrieve data
                string connectionString = @"Data Source=.;Initial Catalog=R49_MyEcommerce_db;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand command = new SqlCommand(@"SELECT * FROM OrderDetails od inner join Orders o on o.OrderID = od.OrderID inner join Products p on p.ProductID = od.ProductID", connection);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "OrderDetails");

                // Load and configure Crystal Report
                ReportDocument reportDocument = new ReportDocument();
                string reportPath = _httpContextAccessor.HttpContext.Request.PathBase;
                reportDocument.Load(reportPath);
                reportDocument.SetDataSource(dataSet);

                // Export report to PDF and return it as a file
                Stream stream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                reportDocument.Close();
                reportDocument.Dispose();
                return File(stream, System.Net.Mime.MediaTypeNames.Application.Pdf);
            }
            catch (Exception ex)
            {
                return Content("<h2>Error:" + ex.Message + "</h2>", "text/html");
            }



        }
    }
}