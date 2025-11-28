using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using System.Linq;
using _2001230507_NhanTuManh_B2.Models;

namespace _2001230507_NhanTuManh_B2.Controllers
{
    public class ReviewsController : Controller
    {
        private FastFeastDbContext db = new FastFeastDbContext();

        // GET: Reviews
        public async Task<ActionResult> Index()
        {
            var reviews = await db.Reviews
                .Include(r => r.Customer)
                .Include(r => r.Product)
                .Include(r => r.Order)
                .OrderByDescending(r => r.ReviewDate)
                .ToListAsync();
            return View(reviews);
        }

        // GET: Reviews/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var review = await db.Reviews
                .Include(r => r.Customer)
                .Include(r => r.Product)
                .Include(r => r.Order)
                .FirstOrDefaultAsync(m => m.ReviewID == id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // GET: Reviews/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName");
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName");
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "OrderID");
            return View();
        }

        // POST: Reviews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int ProductID, int Rating, string Comment)
        {
            // 1. Kiểm tra đăng nhập
            if (Session["CustomerID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                // 2. Tạo đối tượng Review mới
                var review = new Review
                {
                    ProductID = ProductID,
                    CustomerID = (int)Session["CustomerID"],
                    Rating = Rating,
                    Comment = Comment,
                    ReviewDate = DateTime.Now
                };

                // 3. Lưu vào Database
                db.Reviews.Add(review);
                db.SaveChanges();

                // 4. Quay lại trang chi tiết sản phẩm
                TempData["SuccessMessage"] = "Cảm ơn bạn đã đánh giá!";
                return RedirectToAction("Details", "Products", new { id = ProductID });
            }
            return RedirectToAction("Details", "Products", new { id = ProductID });
        }

        // GET: Reviews/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var review = await db.Reviews.FindAsync(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName", review.CustomerID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", review.ProductID);
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "OrderID", review.OrderID);
            return View(review);
        }

        // POST: Reviews/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ReviewID,CustomerID,ProductID,OrderID,Rating,Comment,ReviewDate")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName", review.CustomerID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", review.ProductID);
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "OrderID", review.OrderID);
            return View(review);
        }

        // GET: Reviews/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var review = await db.Reviews
                .Include(r => r.Customer)
                .Include(r => r.Product)
                .Include(r => r.Order)
                .FirstOrDefaultAsync(m => m.ReviewID == id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var review = await db.Reviews.FindAsync(id);
            if (review != null)
            {
                db.Reviews.Remove(review);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}