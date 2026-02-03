using Microsoft.AspNetCore.Mvc;
using MongoDb.WebApi.Documents;
using MongoDb.WebApi.Repositories;
using System.ComponentModel.DataAnnotations;

namespace MongoDb.WebApi.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(User user, CancellationToken cancellationToken)
    {
        var response = await _userRepository.CreateAsync(user, cancellationToken);
        return CreatedAtRoute(nameof(GetUserByIdAsync), new { id = response.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([Required] string id, User user, CancellationToken cancellationToken)
    {
        await _userRepository.UpdateAsync(id, user, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(string id, CancellationToken cancellationToken)
    {
        await _userRepository.DeleteAsync(id, cancellationToken);
        return NoContent();
    }

    [HttpGet("{id}", Name = nameof(GetUserByIdAsync))]
    public async Task<IActionResult> GetUserByIdAsync([Required] string id, CancellationToken cancellationToken)
        => Ok(await _userRepository.GetByIdAsync(id, cancellationToken));

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] int offset = 0, [FromQuery] int fetch = 100, CancellationToken cancellationToken = default)
        => Ok(await _userRepository.GetAllAsync(offset, fetch, cancellationToken));
}
