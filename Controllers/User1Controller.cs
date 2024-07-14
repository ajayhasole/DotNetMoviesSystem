using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Movies.Models;

namespace Movies.Controllers
{
    public class User1Controller : Controller
    {
        private readonly MoviesContext _context;

        public User1Controller(MoviesContext context)
        {
            _context = context;
        }

        // GET: User1
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users1.ToListAsync());
        }

        // GET: User1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user1 = await _context.Users1
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user1 == null)
            {
                return NotFound();
            }

            return View(user1);
        }

        // GET: User1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Email,MobileNumber,Password")] User1 user1)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user1);
        }

        // GET: User1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user1 = await _context.Users1.FindAsync(id);
            if (user1 == null)
            {
                return NotFound();
            }
            return View(user1);
        }

        // POST: User1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Email,MobileNumber,Password")] User1 user1)
        {
            if (id != user1.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!User1Exists(user1.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user1);
        }

        // GET: User1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user1 = await _context.Users1
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user1 == null)
            {
                return NotFound();
            }

            return View(user1);
        }

        // POST: User1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user1 = await _context.Users1.FindAsync(id);
            if (user1 != null)
            {
                _context.Users1.Remove(user1);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool User1Exists(int id)
        {
            return _context.Users1.Any(e => e.Id == id);
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult allMovies()
        {
            List<Models.Movie> list = Movies.Models.Movie.getAll();
            return View(list);
            
        }


        public IActionResult Dashboard()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            string fullName = HttpContext.Session.GetString("FullName");

            if (userId == null || fullName == null)
            {

                return RedirectToAction("Login", "User1");
            }



            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source = (localdb)\ProjectModels; Initial Catalog = Movies; Integrated Security = True;";
            List<UserBokking> list = new List<UserBokking>();
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = " SELECT m.title AS Title,b.date AS BookingDate, b.seatNumber AS SeatNumbe FROM Booking b INNER JOIN Movies m ON b.userid= '"+userId+"';";
                SqlDataReader dr = cmd.ExecuteReader();



                // SELECT m.title AS Title,b.date AS BookingDate, b.seatNumber AS SeatNumbe FROM Booking b INNER JOIN Movies m ON b.movieid = m.Id;

                while (dr.Read())
                {
                    UserBokking userBokking = new UserBokking();
                    // Get user information from the database
                    userBokking.Title = dr.GetString("title");
                   // userBokking.Date = dr.GetDateTime("Date");
                    //userBokking.SeatNumber = dr.GetInt32("seatNumber");



                    list.Add(userBokking);
                   
                    
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
            return View(list);
       }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Password")] User1 user1)
        {

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source = (localdb)\ProjectModels; Initial Catalog = Movies; Integrated Security = True;";
            List<User1> list = new List<User1>();
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from users where email='" + user1.Email + "'and password='" + user1.Password + "';";

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    // Get user information from the database
                    int userId = dr.GetInt32(dr.GetOrdinal("Id"));
                    string fullName = dr.GetString(dr.GetOrdinal("FullName"));

                    // Store user information in session
                    HttpContext.Session.SetInt32("UserId", userId);
                    HttpContext.Session.SetString("FullName", fullName);

                    dr.Close();
                    return RedirectToAction("Dashboard", "User1");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid email or password."); // Add error message to ModelState
                    return View(user1);
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
            return View(list);
        }
    }
}
