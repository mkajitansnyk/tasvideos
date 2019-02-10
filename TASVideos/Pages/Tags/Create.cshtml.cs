﻿using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TASVideos.Data;
using TASVideos.Data.Constants;
using TASVideos.Data.Entity;

namespace TASVideos.Pages.Tags
{
	[RequirePermission(PermissionTo.TagMaintenance)]
	public class CreateModel : BasePageModel
	{
		private readonly ApplicationDbContext _db;

		public CreateModel(ApplicationDbContext db)
		{
			_db = db;
		}

		[TempData]
		public string Message { get; set; }

		[TempData]
		public string MessageType { get; set; }

		[FromRoute]
		public int Id { get; set; }

		[BindProperty]
		public Tag Tag { get; set; } = new Tag();

		public async Task<IActionResult> OnPost()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			_db.Tags.Add(new Tag
			{
				Code = Tag.Code,
				DisplayName = Tag.DisplayName
			});

			try
			{
				MessageType = Styles.Success;
				Message = "Tag successfully created.";
				await _db.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				MessageType = Styles.Danger;
				Message = "Unable to create Tag.";
			}

			return RedirectToPage("Index");
		}
	}
}
