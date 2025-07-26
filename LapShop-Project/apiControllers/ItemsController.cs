
using Bl.Classes;

namespace LapShop_Project.apiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly Iitems oItem;

        public ItemsController(Iitems oItem)
        {
            this.oItem = oItem;
        }


        // GET: api/<ItemsController>
        [HttpGet]
        public ApiResponse Get()
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                oApiResponse.Data = oItem.GetAll();
                oApiResponse.StatusCode = 200;
                oApiResponse.Errors = null;
                oApiResponse.Message = "Done";
                return oApiResponse;
            }
            catch (Exception ex)
            {
                oApiResponse.Data = null;
                oApiResponse.StatusCode = 502;
                oApiResponse.Message = "Not Done";
                oApiResponse.Errors = new List<string> { ex.Message };
                return oApiResponse;
            }

        }

        // GET api/<ItemsController>/
        [HttpGet("{id}")]
        public ApiResponse Get(int id)
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                oApiResponse.Data = oItem.GetById(id);
                oApiResponse.StatusCode = 200;
                oApiResponse.Errors = null;
                oApiResponse.Message = "Done";
                return oApiResponse;
            }
            catch (Exception ex)
            {
                oApiResponse.Data = null;
                oApiResponse.StatusCode = 502;
                oApiResponse.Message = "Not Done";
                oApiResponse.Errors = new List<string> { ex.Message };
                return oApiResponse;
            }
        }

        // GET api/<ItemsController>/
        [HttpGet("GetByCategory/{CategoryId}")]
        public ApiResponse GetByCategory(int CategoryId)
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                oApiResponse.Data = oItem.GetAllItemsData(CategoryId);
                oApiResponse.StatusCode = 200;
                oApiResponse.Errors = null;
                oApiResponse.Message = "Done";
                return oApiResponse;
            }
            catch (Exception ex)
            {
                oApiResponse.Data = null;
                oApiResponse.StatusCode = 502;
                oApiResponse.Message = "Not Done";
                oApiResponse.Errors = new List<string> { ex.Message };
                return oApiResponse;
            }
        }

        // POST api/<ItemsController>
        [HttpPost]
        public ApiResponse Post([FromBody] TbItem itme)
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                oItem.Save(itme);
                oApiResponse.Data = "Saving succeeded...";
                oApiResponse.StatusCode = 200;
                oApiResponse.Errors = null;
                oApiResponse.Message = "Done";
                return oApiResponse;
            }
            catch (Exception ex)
            {
                oApiResponse.Data = "Saving Not succeeded...";
                oApiResponse.StatusCode = 502;
                oApiResponse.Message = "Not Done";
                oApiResponse.Errors = new List<string> { ex.Message };
                return oApiResponse;
            }
        }

        // DELETE api/<ItemsController>/
        [HttpPost("Delete")]
        public ApiResponse Delete([FromBody] int id)
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                oItem.Delete(id);
                oApiResponse.Data = "Deleted succeeded...";
                oApiResponse.StatusCode = 200;
                oApiResponse.Errors = null;
                oApiResponse.Message = "Done";
                return oApiResponse;
            }
            catch (Exception ex)
            {
                oApiResponse.Data = "Deleted Not succeeded...";
                oApiResponse.StatusCode = 502;
                oApiResponse.Message = "Not Done";
                oApiResponse.Errors = new List<string> { ex.Message };
                return oApiResponse;
            }
         

        }
    }
}
