﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Money.BL.Interfaces;
using Money.BL.Models.Account;

namespace Money.API.Controllers;

[Route("[controller]")]
[ApiController]
public class MoneyAccountsController : ControllerBase
{
    private readonly IMoneyAccountService _moneyAccountService;
    private readonly ICurrentUserService _currentUserService;

    public MoneyAccountsController(IMoneyAccountService moneyAccountService, ICurrentUserService currentUserService)
    {
        _moneyAccountService = moneyAccountService;
        _currentUserService = currentUserService;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateMoneyAccount(CreateMoneyAccountModel model)
    {
        var userId = _currentUserService.GetUserId();
        await _moneyAccountService.CreateAccountAsync(model, userId);
        return Created();
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllAccounts()
    {
        var userId = _currentUserService.GetUserId();
        var accounts = await _moneyAccountService.GetAllAccountsAsync(userId);
        return Ok(accounts);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateAccountName(Guid accountId, string newAccountName)
    {
        var userId = _currentUserService.GetUserId();
        await _moneyAccountService.UpdateAccountNameAsync(accountId, userId, newAccountName);
        return Ok();
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteAccount(Guid accountId)
    {
        var userId = _currentUserService.GetUserId();
        await _moneyAccountService.DeleteAccountAsync(accountId, userId);
        return Ok();
    }
}
