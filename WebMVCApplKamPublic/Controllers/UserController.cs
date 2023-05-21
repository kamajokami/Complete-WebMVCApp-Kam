using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using WebMVCApplKamPublic.Data;
using WebMVCApplKamPublic.Models;

namespace WebMVCApplKamPublic.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult UserProfile()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            var list = await _context.Users
                .OrderBy(x => x.Surname)
                .ThenBy(x => x.Name)
                .ThenBy(x => x.UserId)
                .Take(40)
                .ToListAsync();

            return View(list);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null or 0)
            {
                return NotFound();
            }

            var userDetail = await _context.Users.FindAsync(id);

            if (userDetail is null)
            {
                return NotFound();
            }

            return View(userDetail);
        }

        public IActionResult Create()
        {
            User user = new User();

            return View(user);
        }

        [HttpPost] // metodu chci spustit jen v případě, že se odeslal formulář
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            //var user = await _context.Users.Where(x => x.Name == user.Name).FirstOrDefaultAsync();
            //var user = await _context.Users.Where(x => x.Name == user.Name).Count();

            //if(user > 0)
            //{
            //    return RedirectToAction("Index");
            //}

            if (user is null)
            {
                return NotFound("Záznam nenalezen. Zkuste to znovu.");
            }

            if (!ModelState.IsValid)
            {
                return View(user);
            }

            // Ošetřím situace, kdy uživatel nic nezadá nebo zadá jen pár mezer
            // Navíc jméno A nebo B nedává smysl, ale můžu se jmenovat třeba Ed.
            if (string.IsNullOrWhiteSpace(user.Name) || user.Name.Length < 2)
            {
                string errorMessage = $"Jméno musí mít alespoň dva znaky.";

                ModelState.AddModelError("", errorMessage);

                return View(user);
            }

            const int SQLITE_CONSTRAINT_ERROR_CODE = 19;
            const int SQLITE_CONSTRAINT_UNIQUE_ERROR_CODE = SQLITE_CONSTRAINT_ERROR_CODE | (8 << 8);

            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            /* When --> je pro podmíněnost catche,
             * Je InnerException v dbupdateEx typu SqliteException, a má 
             * property SqliteExtendedErrorCode rovnu SQLITE_CONSTRAINT_UNIQUE_ERROR_CODE?*/
            catch (DbUpdateException dbupdateEx) when (dbupdateEx.InnerException is SqliteException &&
            ((SqliteException)dbupdateEx.InnerException).SqliteExtendedErrorCode == SQLITE_CONSTRAINT_UNIQUE_ERROR_CODE)
            {
                string errorMessage = "Email již existuje, zadejte jiný.";

                TempData["error"] = errorMessage;
                ModelState.AddModelError("", errorMessage);
                return View(user);
            }
            catch (Exception ex)
            {
                // Environment poskytuje informace o počítači, který vykonává tento kód
                // Environment.NewLine je multiplatformní \n
                string errorMessage = $"Nastala neošetřená chyba: {Environment.NewLine}{ex}";

                TempData["error"] = errorMessage;
                ModelState.AddModelError("", errorMessage);
                return View(user);
            }

            TempData["success"] = "Person created successfully.";
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null or 0)
            {
                return NotFound();
            }

            var userInDbTable = await _context.Users.FindAsync(id);

            if (userInDbTable is null)
            {
                return NotFound();
            }

            return View(userInDbTable);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(User user)
        {
            string errorMessage;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (user is null)
            {
                return NotFound("Záznam nenalezen. Zkuste to znovu.");
            }

            if (string.IsNullOrWhiteSpace(user.Name) || user.Name.Length < 2)
            {
                errorMessage = $"Jméno musí mít alespoň dva znaky.";

                ModelState.AddModelError("", errorMessage);

                return View(user);
            }

            const int SQLITE_CONSTRAINT_ERROR_CODE = 19;
            const int SQLITE_CONSTRAINT_UNIQUE_ERROR_CODE = SQLITE_CONSTRAINT_ERROR_CODE | (8 << 8);

            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbupdateEx) when (dbupdateEx.InnerException is SqliteException
            {
                SqliteExtendedErrorCode: SQLITE_CONSTRAINT_UNIQUE_ERROR_CODE
            })
            {
                errorMessage = "Email již existuje, zadejte jiný.";

                TempData["error"] = errorMessage;
                ModelState.AddModelError("", errorMessage);
                return View(user);
            }
            catch (Exception ex)
            {
                errorMessage = $"Nastala neošetřená chyba: {Environment.NewLine}{ex}";

                TempData["error"] = errorMessage;
                ModelState.AddModelError("", errorMessage);
                return View(user);
            }

            TempData["success"] = "Person updated successfully.";
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null or 0)
            {
                return NotFound();
            }

            var userInDb = await _context.Users.FindAsync(id);

            if (userInDb is null)
            {
                return NotFound();
            }

            TempData.Keep();

            return View(userInDb);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(User user)
        {
            if (user is null)
            {
                return NotFound();
            }

            try
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;

                TempData["error"] = errorMessage;
                ModelState.AddModelError("", errorMessage);
                return View(user);
            }

            TempData["success"] = $"{user.Name} was deleted successfully.";

            return RedirectToAction(nameof(Index));
        }
    }
}
