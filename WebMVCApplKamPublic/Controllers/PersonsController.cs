using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Text;
using WebMVCApplKamPublic.Data;
using WebMVCApplKamPublic.Models;

namespace WebMVCApplKamPublic.Controllers
{
    public class PersonsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _context.Persons
                .OrderBy(x => x.Surname)
                .ThenBy(x => x.Name)
                .ThenBy(x => x.Id)
                .Take(40)
                .ToListAsync();

            return View(list);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if(id is null or 0) 
            {
                return NotFound();
            }

            var personToDetail = await _context.Persons.FindAsync(id);

            if(personToDetail is null)
            {
                return NotFound();
            }

            return View(personToDetail);
        }

        public IActionResult Create()
        {
            Person person = new Person();

            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Person person)
        {
            if (person is null)
            {
                return NotFound("Záznam nenalezen. Zkuste to znovu.");
            }

            if (!ModelState.IsValid)
            {
                return View(person);
            }

            if (string.IsNullOrWhiteSpace(person.Name) || person.Name.Length < 2)
            {
                string errorMessage = $"Jméno musí mít alespoň dva znaky.";

                ModelState.AddModelError("", errorMessage);

                return View(person);
            }

            const int SQLITE_CONSTRAINT_ERROR_CODE = 19;
            const int SQLITE_CONSTRAINT_UNIQUE_ERROR_CODE = SQLITE_CONSTRAINT_ERROR_CODE | (8 << 8);

            try
            {
                _context.Persons.Add(person);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbupdateEx) when (dbupdateEx.InnerException is SqliteException
            {
                SqliteExtendedErrorCode: SQLITE_CONSTRAINT_UNIQUE_ERROR_CODE
            })
            {
                string errorMessage = "Email již existuje, zadejte jiný.";

                TempData["error"] = errorMessage;
                ModelState.AddModelError("", errorMessage);
                return View(person);
            }
            catch (Exception ex)
            {
                string errorMessage = $"Nastala neošetřená chyba: {Environment.NewLine}{ex}";

                TempData["error"] = errorMessage;
                ModelState.AddModelError("", errorMessage);
                return View(person);
            }

            TempData["success"] = "Person created successfully.";
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if(id is null or 0)
            {
                return NotFound();
            }

            var pesonInDbTable = await _context.Persons.FindAsync(id);

            if(pesonInDbTable is null)
            {
                return NotFound();
            }

            return View(pesonInDbTable);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Person person)
        {
            string errorMessage;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (person is null)
            {
                return NotFound("Záznam nenalezen. Zkuste to znovu.");
            }

            if (string.IsNullOrWhiteSpace(person.Name) || person.Name.Length < 2)
            {
                errorMessage = $"Jméno musí mít alespoň dva znaky.";

                ModelState.AddModelError("", errorMessage);

                return View(person);
            }

            const int SQLITE_CONSTRAINT_ERROR_CODE = 19;
            const int SQLITE_CONSTRAINT_UNIQUE_ERROR_CODE = SQLITE_CONSTRAINT_ERROR_CODE | (8 << 8);

            try
            {
                _context.Persons.Update(person);
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
                return View(person);
            }
            catch (Exception ex)
            {
                errorMessage = $"Nastala neošetřená chyba: {Environment.NewLine}{ex}";

                TempData["error"] = errorMessage;
                ModelState.AddModelError("", errorMessage);
                return View(person);
            }

            TempData["success"] = "Person updated successfully.";
            return RedirectToAction(nameof(Index));      
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if(id is null or 0)
            {
                return NotFound();
            }

            var personInDb = await _context.Persons.FindAsync(id);

            if(personInDb is null)
            {
                return NotFound();
            }

            TempData.Keep();

            return View(personInDb);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Person person)
        {
            if (person is null)
            {
                return NotFound();
            }

            try
            {
                _context.Persons.Remove(person);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;

                TempData["error"] = errorMessage;
                ModelState.AddModelError("", errorMessage);
                return View(person);
            }
            
            TempData["success"] = $"{person.Name} was deleted successfully.";

            return RedirectToAction(nameof(Index));
        }
    }
}
