

namespace Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ILapShop<TbCategory> oClsCategories;

        public CategoriesController(ILapShop<TbCategory> Category) => oClsCategories = Category;



        // GET: api/<ItemsController>
        [HttpGet]
        public ApiResponse Get()
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                oApiResponse.Data = oClsCategories.GetAll();
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
                oApiResponse.Data = oClsCategories.GetById(id);
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
        public ApiResponse Post([FromBody] TbCategory category)
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                oClsCategories.Save(category);
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
                oClsCategories.Delete(id);
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
