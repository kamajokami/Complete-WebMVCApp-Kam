using WebMVCApplKamPublic.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebMVCApplKamPublic.Data;
using WebMVCApplKamPublic.Models;
using Microsoft.Data.Sqlite;

namespace WebMVCApplKamPublic.Controllers
{
    public class PriceViewModelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PriceViewModelController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _context.ComodityPrices
                .OrderBy(x => x.Title)
                .ThenBy(x => x.Id)
                .Take(40)
                .ToListAsync();

            return View(list);
        }


        // Vyber více možností z Checkboxu
        public IActionResult IndexCheckBox()
        {
            var model = new PriceViewModel()
            {
                Checkboxes = new List<CheckboxOption>
                {
                    new CheckboxOption
                    {
                        IsChecked = true,
                        Description = "Option 1",
                        Value = "Option1"
                    },

                    new CheckboxOption
                    {
                        IsChecked = false,
                        Description = "Option 2",
                        Value = "Option2"
                    },

                    new CheckboxOption
                    {
                        IsChecked = false,
                        Description = "Option 3",
                        Value = "Option3"
                    }
                }
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult IndexCheckBox(PriceViewModel model)
        {
            return RedirectToAction("IndexCheckBox");
        }


        #region Index method
        //public async Task<IActionResult> Index()
        //{
        //    var list = await _context.ComodityPrices
        //        .OrderBy(x => x.Title)
        //        .ThenBy(x => x.Id)
        //        .Take(40)
        //        .ToListAsync();

        //    var model = new PriceViewModel()
        //    {
        //        IsSale = false,
        //        IsCommodityActive = true,
        //        IsPropertyOwner = false,
        //        SendConsent = true
        //    };

        //    ViewData["model"] = model;


        //    return View(list);
        //}

        #endregion Index method



        #region Index without CheckControl
        //public async Task<IActionResult> Index()
        //{
        //    var list = await _context.ComodityPrices
        //        .OrderBy(x => x.Title)
        //        .ThenBy(x => x.Id)
        //        .Take(40)
        //        .ToListAsync();

        //    return View(list);
        //}

        #endregion Index without CheckControl




        #region Načtení Checkboxu 
        //public IActionResult Index()
        //{
        //    var model = new PriceViewModel()
        //    {
        //        IsSale = false,
        //        IsCommodityActive = true,
        //        IsPropertyOwner = false,
        //        SendConsent = true
        //    };



        //    return View(model);
        //}

        #endregion Načtení Checkboxu 





        public IActionResult Create()
        {
            PriceViewModel priceViewModel = new PriceViewModel()
            {
                IsSale = true,
                IsCommodityActive = false,
                IsCommodityActiveCheckboxDescription = "Zboží je dostupné",
                IsPropertyOwner = false,
                IsBuyerMan = false,
                IsBuyerWoman = false,
                SendConsent = true
            };

            return View(priceViewModel);
        }


        [HttpPost] // metodu chci spustit jen v případě, že se odeslal formulář
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PriceViewModel priceViewModel)
        {
            if (priceViewModel is null)
            {
                return NotFound("Záznam nenalezen. Zkuste to znovu.");
            }

            if (!ModelState.IsValid)
            {
                return View(priceViewModel);
            }

            // Ošetřím situace, kdy uživatel nic nezadá nebo zadá jen pár mezer
            // Navíc titulek A nebo B nedává smysl.
            if (string.IsNullOrWhiteSpace(priceViewModel.Title) || priceViewModel.Title.Length < 2)
            {
                string errorMessage = $"Předmět musí mít alespoň dva znaky.";

                ModelState.AddModelError("", errorMessage);

                return View(priceViewModel);
            }

            const int SQLITE_CONSTRAINT_ERROR_CODE = 19;
            const int SQLITE_CONSTRAINT_UNIQUE_ERROR_CODE = SQLITE_CONSTRAINT_ERROR_CODE | (8 << 8);

            try
            {
                _context.ComodityPrices.Add(priceViewModel);
                await _context.SaveChangesAsync();
            }
            /* When --> je pro podmíněnost catche,
             * Je InnerException v dbupdateEx typu SqliteException, a má 
             * property SqliteExtendedErrorCode rovnu SQLITE_CONSTRAINT_UNIQUE_ERROR_CODE?*/
            catch (DbUpdateException dbupdateEx) when (dbupdateEx.InnerException is SqliteException &&
            ((SqliteException)dbupdateEx.InnerException).SqliteExtendedErrorCode == SQLITE_CONSTRAINT_UNIQUE_ERROR_CODE)
            {
                string errorMessage = "Předmět již existuje, zadejte jiný.";

                TempData["error"] = errorMessage;
                ModelState.AddModelError("", errorMessage);
                return View(priceViewModel);
            }
            catch (Exception ex)
            {
                string errorMessage = $"Nastala neošetřená chyba: {Environment.NewLine}{ex}";

                TempData["error"] = errorMessage;
                ModelState.AddModelError("", errorMessage);
                return View(priceViewModel);
            }

            TempData["success"] = "Record created successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
