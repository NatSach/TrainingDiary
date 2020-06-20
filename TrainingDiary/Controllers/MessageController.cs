using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainingDiary.Enums;
using TrainingDiary.Models;
using TrainingDiary.Services;
using TrainingDiary.ViewModels;

namespace TrainingDiary.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private readonly IUserService _userService;
        private readonly IDbContextService _dbContextService;

        public MessageController(IUserService userService, IDbContextService dbContextService)
        {
            _userService = userService;
            _dbContextService = dbContextService;
        }

        // GET: Message
        public async Task<IActionResult> Index()
        {
            User user = await _userService.GetUser(User);

            var messageVM = new MessageViewModel();

            messageVM.Inbox = await _dbContextService.GetInbox(user.Id);
            messageVM.Outbox = await _dbContextService.GetOutbox(user.Id);        

            if (user is Player)
            {
                messageVM.Requests = await _dbContextService.GetRequestsForSender(user.Id);
            }
            else if (user is Coach)
            {
                messageVM.Requests = await _dbContextService.GetRequestsForReceiver(user.Id);
            }
            if(TempData.ContainsKey("ActiveTab"))
            {
                ViewBag.TabHref = MessageTabHref.Request;
                TempData.Clear();
            }
            else
            {
                ViewBag.TabHref = MessageTabHref.Inbox;
            }
            return View(messageVM);
        }
        public async Task<ActionResult> AcceptRequest(int id)
        {
            Request request = await _dbContextService.GetRequestByIdAsync(id);
            request.RequestStatus = RequestStatus.Confirmed;
            request.IsReadBySender = false;
            await _dbContextService.UpdateObjectInDb(request);
            await _dbContextService.CreateRelationBetweenCoachAndPlayer(request);
            TempData["ActiveTab"] = "Request";
            return RedirectToAction(nameof(Index));
        }
        public async Task<ActionResult> DeclineRequest(int id)
        {
            Request request = await _dbContextService.GetRequestByIdAsync(id);
            request.RequestStatus = RequestStatus.Canceled;
            request.IsReadBySender = false;
            await _dbContextService.UpdateObjectInDb(request);
            TempData["ActiveTab"] = "Request";
            return RedirectToAction(nameof(Index));
        }

        // GET: Message/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Message message = await _dbContextService.GetMessageByIdAsync((int)id);
            if (message == null)
            {
                return NotFound();
            }

            string userId = _userService.GetUserId(User);
            if (message.Receiver.Id == userId)
            {
                message.IsReadByReceiver = true;
                await _dbContextService.UpdateObjectInDb(message);
            }
            else if(message.Sender.Id == userId)
            {
                message.IsReadBySender = true;
                await _dbContextService.UpdateObjectInDb(message);
            }
            else
            {
                return NotFound();
            }

            if(message is Request)
            {
                TempData["ActiveTab"] = "Request";
            }

            return View(message);
        }

        // GET: Message/CreateMessage
        public IActionResult CreateMessage()
        {
            return View();
        }

        [Authorize(Roles = Role.Player)]
        public IActionResult CreateRequest()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRequest([Bind("Email")] CreateRequestViewModel createRequestVM)
        {
            if(ModelState.IsValid)
            {
                User receiver = await _userService.GetCoachByEmail(createRequestVM.Email);

                if (receiver == null)
                {
                    ModelState.AddModelError(string.Empty, "Coach with this email address doesn't exist.");
                    return View(createRequestVM);
                }
                User currentUser = await _userService.GetUser(User);
                Request message = new Request(currentUser.FirstName, currentUser.LastName)
                {
                    Receiver = receiver,
                    Sender = currentUser,
                    SendDate = DateTime.Now
                };

                await _dbContextService.AddObjectToDb(message);
                return RedirectToAction(nameof(Index));
            }
            return View(createRequestVM);
        }

        // POST: Message/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMessage([Bind("Title,Content,Email")] CreateMessageViewModel createMessageVM)
        {
            if (ModelState.IsValid)
            {
                User receiver = await _userService.GetUserByEmail(createMessageVM.Email);

                if(receiver == null)
                {
                    ModelState.AddModelError(string.Empty, "User with this email address doesn't exist.");
                    return View(createMessageVM);
                }

                User currentUser = await _userService.GetUser(User);

                Message message = new Message()
                {
                    Title = createMessageVM.Title,
                    Content = createMessageVM.Content,
                    Receiver = receiver,
                    Sender = currentUser,
                    SendDate = DateTime.Now
                };

                await _dbContextService.AddObjectToDb(message);
                return RedirectToAction(nameof(Index));
            }
            return View(createMessageVM);
        }

        // GET: Message/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _dbContextService.FindMessageById((int)id);
            if (message == null)
            {
                return NotFound();
            }
            return View(message);
        }

        // POST: Message/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Content,SendDate,ReceiveDate")] Message message)
        {
            if (id != message.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _dbContextService.UpdateObjectInDb(message);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_dbContextService.MessageExists(message.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(message);
        }

        // GET: Message/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _dbContextService.GetMessageByIdAsync((int)id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // POST: Message/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var message = await _dbContextService.FindMessageById((int)id);
            await _dbContextService.RemoveMessageFromDb(message);
            return RedirectToAction(nameof(Index));
        }
    }
}
