using Microservices.Web.Models;
using Microservices.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Microservices.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDto>? list = null;

            ResponseDto? response = await _couponService.GetAllCouponsAsync();

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
            }

            else
            {
                TempData["error"] = response?.Message;
            }

            return View(list);
        }
        [HttpGet]
        public async Task<IActionResult> CouponCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponDto couponDto)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _couponService.CreateCouponsAsync(couponDto);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Coupon created successfully";
                    return RedirectToAction(nameof(CouponIndex));
                }

                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return View(couponDto);
        }
        [HttpGet]
        public async Task<IActionResult> CouponDelete(int id)
        {
            TempData["success"] = "Coupon deleted successfully";
            await _couponService.DeleteCouponsAsync(id);
            return RedirectToAction(nameof(CouponIndex));
        }
    }
}
