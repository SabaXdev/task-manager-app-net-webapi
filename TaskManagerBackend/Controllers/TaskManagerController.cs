using Microsoft.AspNetCore.Mvc;
using TaskManagerBackend.Interfaces;

namespace TaskManagerBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskManagerController : ControllerBase
    {
        private readonly ITaskManagerServices _taskManagerService;

        public TaskManagerController(ITaskManagerServices service)
        {
            _taskManagerService = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTaskAsync(CreateTaskManagerRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdTask = await _taskManagerService.CreateTaskManagerAsync(request);
                return Ok(new { message = $"Task item successfully created." , data = createdTask});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = 
                "An error occurred while creating Task Item", error = ex.Message});
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var TaskItems = await _taskManagerService.GetAllAsync();
                if (TaskItems == null || !TaskItems.Any())
                {
                    return Ok( new { message = "No Task items were found. "});
                }
                return Ok( new { message = "Task items successfully returned. ", data = TaskItems});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving all Task items", error = ex.Message });
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var TaskItem = await _taskManagerService.GetByIdAsync(id);
                if (TaskItem == null)
                {
                    return Ok( new { message = $"No Task items were found with given id {id}."});
                }
                return Ok( new { message = $"Task item with id {id} successfully returned. ", data = TaskItem});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while retrieving the Task item with Id {id}.", error = ex.Message});
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTaskItemAsync(Guid id, UpdateTaskManagerRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var TaskItem = await _taskManagerService.GetByIdAsync(id);
                if (TaskItem == null)
                {
                    return NotFound(new { message = $"Task Item with given id {id} not found."});
                }
                
                var updatedTaskItem = await _taskManagerService.UpdateTaskManagerAsync(id, request);
                return Ok(new { message = $"Task item with given id {id} were successfuly updated.", data = updatedTaskItem});
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while updating Task item with id {id}", error = ex.Message });
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTaskItemAsync(Guid id)
        {
            try{
                var TaskItem = await _taskManagerService.GetByIdAsync(id);
                if (TaskItem == null)
                {
                    return NotFound(new { message = $"Task Item with given id {id} not found."});
                }

                await _taskManagerService.DeleteTaskManagerAsync(id);
                return Ok(new { message = $"Task item with given id {id} were successfuly deleted." });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while deleting Task item with id {id}", error = ex.Message });
            }
        }

    }
}
