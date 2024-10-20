using Microsoft.AspNetCore.Mvc;
using Simple_Blog.Model.Dtos;
using Simple_Blog.Model.Entities;
using Simple_Blog.Services;

namespace Simple_Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly ILogger<BlogsController> _logger;

        public BlogsController(IBlogService blogService, ILogger<BlogsController> logger)
        {
            _blogService = blogService;
            _logger = logger;
        }

        // GET: api/Blogs
        [HttpGet]
        [Route("Load/")]
        public async Task<ActionResult<List<BlogDto>>> LoadBlog()
        {
            var itemList = new List<BlogDto>();
            try
            {
                var itemModelList = _blogService.LoadAll().ToList();
                itemList = itemModelList.Select(x => BlogDto.GetBlogDto(x)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(Info.CommonErrorMessage);
            }

            return Ok(itemList);
        }

        // GET: api/SearchBlog
        [HttpGet]
        [Route("SearchBlogs/")]
        public async Task<ActionResult<SearchBlogDto?>> SearchBlogs([FromQuery] int start, [FromQuery] int length, [FromQuery] string searchText = "", [FromQuery] bool withDeleted = false, [FromQuery] bool? isActive = null, [FromQuery] string hasIds = "")
        {
            SearchBlogDto? searchBlogDto = null;
            try
            {
                (var total, var itemModelList) = await _blogService.SearchAsync(start, length, searchText, withDeleted, isActive, hasIds);
                var itemList = itemModelList.Select(x => BlogDto.GetBlogDto(x)).ToList();
                searchBlogDto = new SearchBlogDto
                {
                    TotalBlog = total,
                    BlogDtoList = itemList,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(Info.CommonErrorMessage);
            }

            return Ok(searchBlogDto);
        }

        // GET: api/Blogs/5
        // [HttpGet("{id}")]
        [HttpGet]
        [Route("Get/{id}")]
        public async Task<ActionResult<Blog>> GetBlog(long id)
        {
            BlogDto? blog = null;

            try
            {
                var blogModel = await _blogService.FindAsync(id) ?? throw new KeyNotFoundException("Requested blog is not found.");
                if (blogModel != null)
                    blog = BlogDto.GetBlogDto(blogModel);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

            return Ok(blog);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(CreateBlogDto request)
        {
            BlogDto? blog = null;
            try
            {
                if (request == null)
                    throw new KeyNotFoundException("No request data found.");


                var blogModel = await _blogService.CreateAsync(request.GetBlogModel());

                if (blogModel.Id > 0)
                    blog = BlogDto.GetBlogDto(blogModel);

                if (blog == null)
                    throw new KeyNotFoundException("Unable to create blog.");
            }
            catch (KeyNotFoundException ex)
            {
                 return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(Info.CommonErrorMessage);
            }

            return Ok(blog);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update(UpdateBlogDto request)
        {
            BlogDto? blog = null;
            try
            {
                if (request == null)
                    throw new KeyNotFoundException("No request data found.");

                var blogModel = await _blogService.UpdateAsync(request.GetBlogModel()) ?? throw new KeyNotFoundException("Unable to create blog.");

                blog = BlogDto.GetBlogDto(blogModel);
            }
            catch (KeyNotFoundException ex)
            {
                 return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(Info.CommonErrorMessage);
            }

            return Ok(blog);
        }

        // DELETE: api/Blogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(long id)
        {
            try
            {
                await _blogService.DeleteBlog(id);
            }
            catch (KeyNotFoundException ex)
            {
                 return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(Info.CommonErrorMessage);
            }

            return Ok();
        }
    }
}
