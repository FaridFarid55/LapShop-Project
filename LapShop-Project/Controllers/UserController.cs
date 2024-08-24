

namespace LapShop_Project.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signManager;
        private ApplicationUser user;

        // constrictor
        public UserController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signManager)
        {
            this._userManager = _userManager;
            this._signManager = _signManager;
        }

        //
        [HttpGet]
        public async Task<IActionResult> LoginOut()
        {
            await _signManager.SignOutAsync();
            return Redirect("~/");
        }

        //
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            Login login = new Login
            {
                ReturnUrl = returnUrl
            };
            return View(login);
        }
        [HttpPost]
        public async Task<IActionResult> Login(Login loginModel)
        {
            // initialize
            user = new ApplicationUser()
            {
                Email = loginModel.Email,
                UserName = loginModel.Email.Substring(0, loginModel.Email.IndexOf('@'))
            };

            try
            {
                // search on password in database
                var loginResult = await _signManager.PasswordSignInAsync(user.Email, loginModel.Password, true, true);
                if (loginResult.Succeeded)
                {
                    string sReturnUrl = ClsUiHelper.Url(loginModel.ReturnUrl);
                    return Redirect(sReturnUrl);
                }
                else
                {
                    return View("Login", loginModel);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Big Error");
            }

            return View();
        }

        //
        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            UserModel userModel = new UserModel
            {
                ReturnUrl = returnUrl
            };
            return View(userModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", userModel);
            }

            // initialize
            user = new ApplicationUser()
            {
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Email = userModel.Email,
                UserName = userModel.Email.Substring(0, userModel.Email.IndexOf('@'))
            };

            try
            {
                // password
                var resulte = await _userManager.CreateAsync(user, userModel.Password);

                if (resulte.Succeeded)
                {
                    //  #password#
                    var loginResult = await _signManager.PasswordSignInAsync(user, userModel.Password, true, true);

                    // set default access 
                    var myUser = await _userManager.FindByEmailAsync(user.Email);
                    await _userManager.AddToRoleAsync(myUser, "Customer");

                    // check url
                    string sReturnUrl = ClsUiHelper.Url(userModel.ReturnUrl);
                    return Redirect(sReturnUrl);
                }
                else
                {
                    ViewBag.Errors = resulte.Errors;
                    return View();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Big Error");
            }

            return View();
        }


        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
