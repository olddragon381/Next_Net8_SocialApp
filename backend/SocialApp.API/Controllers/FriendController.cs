using CSharpFunctionalExtensions.ValueTasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialApp.Application.DTOs;
using SocialApp.Application.Interfaces;
using SocialApp.Application.UseCase.Friend;

namespace SocialApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FriendController : ControllerBase
    {
        private readonly IFriendService _friendService;

        public FriendController(IFriendService friendService)
        {
            _friendService = friendService;
        }
        [Authorize]
        [HttpPost("request")]
        public async Task<IActionResult> SendFriendRequest([FromBody] SendRequestDto dto)
        {
            var result = await _friendService.SendRequestAsync(dto.SenderId, dto.ReceiverId);
            if (result.IsSuccess)
                return Ok(new { message = "Thành công" });
            else
                return BadRequest(result.Error);

        }
        [Authorize]
        [HttpPost("accept/{requestId}")]
        public async Task<IActionResult> Accept(string requestId)
        {
            var result = await _friendService.AcceptRequestAsync(requestId);
            if (result.IsSuccess)
                return Ok(new { message = "Thành công" });
            else
                return BadRequest(result.Error);
        }
        [Authorize]
        [HttpPost("reject/{requestId}")]
        public async Task<IActionResult> Reject(string requestId)
        {
            var result = await _friendService.RejectRequestAsync(requestId);
            return Ok(new { message = "Thành công" });
        }
        [Authorize]
        [HttpGet("received/{userId}")]
        public async Task<IActionResult> GetReceived(string userId)
        {
            var list = await _friendService.GetReceivedRequests(userId);
            return Ok(list);
        }
        [Authorize]
        [HttpGet("sent/{userId}")]
        public async Task<IActionResult> GetSent(string userId)
        {
            var list = await _friendService.GetSentRequests(userId);
            return Ok(list);
        }

       
        [HttpGet("{userId}/friends")]
        public async Task<IActionResult> GetFriends(string userId)
        {
            var result = await _friendService.GetFriendsAsync(userId);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("unfriend")]
        public async Task<IActionResult> Unfriend([FromBody] UnfriendDTO dto)
        {
            if ( dto == null)
            {
                return BadRequest(new { message = "That bai" });
            }
            var result = await _friendService.UnfriendAsync(dto.UserId1, dto.UserId2);
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(result.Error);
        }
    }
}
