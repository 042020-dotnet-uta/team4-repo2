using CharSheet.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;
using Xunit;

namespace UnitTests
{
	public class DbContextAdditionTesting
	{
		[Fact]
		public void AddUserWithDbContext()
		{
			//  Arrange
			var options = new DbContextOptionsBuilder<CharSheetContext>()
				.UseInMemoryDatabase(databaseName: "AddUser")
				.Options;

			//  Act
			using(var context = new CharSheetContext(options))
			{
				context.Users.Add(new CharSheet.Domain.User
				{
					UserId = new Guid(),
					Username = "bupkis",
					Email = "someone@domain.com"
				}) ;
				context.SaveChanges();
			}

			//  Assert
			using(var context = new CharSheetContext(options))
			{
				var users = context.Users.ToList();
				Assert.Single(users);
				Assert.Equal("bupkis", users[0].Username);
			}

		}

		[Fact]
		public void AddFormLabelWithDbContext()
		{
			//  Arrange
			var options = new DbContextOptionsBuilder<CharSheetContext>()
				.UseInMemoryDatabase(databaseName: "AddFormLabel")
				.Options;

			//  Act
			using (var context = new CharSheetContext(options))
			{
				context.FormLabels.Add(new CharSheet.FormLabel
				{
					FormLabelId = new Guid(),
					FormTemplateId = new Guid(),
					Index = 1,
					Value = "someValue"
				}) ; 
				context.SaveChanges();
			}

			//  Assert
			using (var context = new CharSheetContext(options))
			{
				var formLabels = context.FormLabels.ToList();
				Assert.Single(formLabels);
				Assert.Equal("someValue", formLabels[0].Value);
				Assert.Equal(1, formLabels[0].Index);
				formLabels[0].FormLabelId.Equals(new Guid());
				formLabels[0].FormTemplateId.Equals(new Guid());
			}
		}

		[Fact]
		public void AddFormPositionWithDbContext()
		{
			//  Arrange
			var options = new DbContextOptionsBuilder<CharSheetContext>()
				.UseInMemoryDatabase(databaseName: "AddFormPosition")
				.Options;

			//  Act
			using (var context = new CharSheetContext(options))
			{
				context.FormPositions.Add(new CharSheet.Domain.FormPosition
				{
					FormPostionId = new Guid(),
					FormTemplateId = new Guid(),
					OffsetTop = 20,
					OffsetLeft= 30,
					XPos = 80,
					YPos = 90,
					Width = 300,
					Height = 500
				});
				context.SaveChanges();
			}

			//  Assert
			using (var context = new CharSheetContext(options))
			{
				var formPositions = context.FormPositions.ToList();
				Assert.Single(formPositions);
				formPositions[0].FormPostionId.Equals(new Guid());
				formPositions[0].FormTemplateId.Equals(new Guid());
				Assert.Equal(20, formPositions[0].OffsetTop);
				Assert.Equal(30, formPositions[0].OffsetLeft);
				Assert.Equal(80, formPositions[0].XPos);
				Assert.Equal(90, formPositions[0].YPos);
				Assert.Equal(300, formPositions[0].Width);
				Assert.Equal(500, formPositions[0].Height);

			}
		}

		[Fact]
		public void AddFormInputWithDbContext()
		{
			//  Arrange
			var options = new DbContextOptionsBuilder<CharSheetContext>()
				.UseInMemoryDatabase(databaseName: "AddFormInput")
				.Options;

			//  Act
			using (var context = new CharSheetContext(options))
			{
				context.FormInputs.Add(new CharSheet.Domain.FormInput
				{
					FormInputId = new Guid(),
					FormInputGroupId = new Guid(),
					Index = 1,
					Value = "someValue"
				});
				context.SaveChanges();
			}

			//  Assert
			using (var context = new CharSheetContext(options))
			{
				var formInputs = context.FormInputs.ToList();
				Assert.Single(formInputs);
				formInputs[0].FormInputId.Equals(new Guid());
				formInputs[0].FormInputGroupId.Equals(new Guid());
				Assert.Equal(1, formInputs[0].Index);
				Assert.Equal("someValue", formInputs[0].Value);

			}
		}

		[Fact]
		public void AddFormTemplateWithDbContext()
		{
			//  Arrange
			var options = new DbContextOptionsBuilder<CharSheetContext>()
				.UseInMemoryDatabase(databaseName: "AddFormTemplates")
				.Options;

			Guid formId = new Guid();
			Guid templateId = new Guid();
			//  Act
			using (var context = new CharSheetContext(options))
			{

				//  First add Form position then add Form template
				context.FormPositions.Add(new CharSheet.Domain.FormPosition
				{
					FormPostionId = formId,
					FormTemplateId = templateId,
					OffsetTop = 20,
					OffsetLeft = 30,
					XPos = 80,
					YPos = 90,
					Width = 300,
					Height = 500
				});

				context.FormLabels.Add(new CharSheet.FormLabel
				{
					FormLabelId = new Guid(),
					FormTemplateId = new Guid(),
					Index = 1,
					Value = "someValue"
				});

				context.SaveChanges();

				context.FormTemplates.Add(new CharSheet.Domain.FormTemplate
				{
					FormTemplateId = new Guid(),
					TemplateId = new Guid(),
					FormPositionId = context.FormPositions.Where(f => f.FormTemplateId == templateId).Select(i => i.FormPostionId).FirstOrDefault(),
					FormPosition = context.FormPositions.Where(f => f.FormTemplateId == templateId).FirstOrDefault(),
					Type = "Type",
					Title = "Title",
					FormLabels = context.FormLabels.ToList()
				});

				context.SaveChanges();
			}

			//  Assert
			using (var context = new CharSheetContext(options))
			{
				var formTemplates = context.FormTemplates.ToList();
				var formPosition = context.FormPositions.FirstOrDefault();
				var formLabels = context.FormLabels.ToList();
				Assert.Single(formTemplates);
				formTemplates[0].FormTemplateId.Equals(new Guid());
				formTemplates[0].TemplateId.Equals(new Guid());
				formTemplates[0].FormPositionId.Equals(new Guid());
				Assert.Equal(formPosition, formTemplates[0].FormPosition);
				Assert.Equal("Type", formTemplates[0].Type);
				Assert.Equal("Title", formTemplates[0].Title);
				Assert.Equal(formLabels, formTemplates[0].FormLabels);

			}

		}

		[Fact]
		public void AddFormInputGroupWithDbContext()
		{
			//  Arrange
			var options = new DbContextOptionsBuilder<CharSheetContext>()
				.UseInMemoryDatabase(databaseName: "AddFormInputGroup")
				.Options;

			Guid formId = new Guid();
			Guid templateId = new Guid();
			//  Act
			using (var context = new CharSheetContext(options))
			{

				//  First add Form position then add Form template
				context.FormPositions.Add(new CharSheet.Domain.FormPosition
				{
					FormPostionId = formId,
					FormTemplateId = templateId,
					OffsetTop = 20,
					OffsetLeft = 30,
					XPos = 80,
					YPos = 90,
					Width = 300,
					Height = 500
				});

				context.FormLabels.Add(new CharSheet.FormLabel
				{
					FormLabelId = new Guid(),
					FormTemplateId = new Guid(),
					Index = 1,
					Value = "someValue"
				});
				context.FormInputs.Add(new CharSheet.Domain.FormInput
				{
					FormInputId = new Guid(),
					FormInputGroupId = new Guid(),
					Index = 1,
					Value = "someValue"
				});

				context.SaveChanges();

				context.FormTemplates.Add(new CharSheet.Domain.FormTemplate
				{
					FormTemplateId = new Guid(),
					TemplateId = new Guid(),
					FormPositionId = context.FormPositions.Where(f => f.FormTemplateId == templateId).Select(i => i.FormPostionId).FirstOrDefault(),
					FormPosition = context.FormPositions.Where(f => f.FormTemplateId == templateId).FirstOrDefault(),
					Type = "Type",
					Title = "Title",
					FormLabels = context.FormLabels.ToList()
				});

				context.SaveChanges();

				context.FormInputGroups.Add(new CharSheet.Domain.FormInputGroup
				{
					FormInputGroupId = new Guid(),
					SheetId = new Guid(),
					FormTemplateId = context.FormTemplates.Where(c => c.TemplateId == templateId).Select(c => c.TemplateId).FirstOrDefault(),
					FormTemplate = context.FormTemplates.Where(c => c.TemplateId == templateId).FirstOrDefault(),
					FormInputs = context.FormInputs.ToList()
				}) ;

				context.SaveChanges();
			}

			//  Assert
			using (var context = new CharSheetContext(options))
			{
				var inputGroups = context.FormInputGroups.ToList();
				var formTemplate = context.FormTemplates.FirstOrDefault();
				var formInputs = context.FormInputs.ToList();
				Assert.Single(inputGroups);
				inputGroups[0].FormInputGroupId.Equals(new Guid());
				inputGroups[0].SheetId.Equals(new Guid());
				Assert.Equal(formTemplate, inputGroups[0].FormTemplate);
				Assert.Equal(formInputs, inputGroups[0].FormInputs);

			}
		}

		[Fact]
		public void AddSheetWithDbContext()
		{
			//  Arrange
			var options = new DbContextOptionsBuilder<CharSheetContext>()
				.UseInMemoryDatabase(databaseName: "AddSheet")
				.Options;

			Guid formId = new Guid();
			Guid templateId = new Guid();
			//  Act
			using (var context = new CharSheetContext(options))
			{

				//  First add Form position then add Form template
				context.FormPositions.Add(new CharSheet.Domain.FormPosition
				{
					FormPostionId = formId,
					FormTemplateId = templateId,
					OffsetTop = 20,
					OffsetLeft = 30,
					XPos = 80,
					YPos = 90,
					Width = 300,
					Height = 500
				});

				context.FormLabels.Add(new CharSheet.FormLabel
				{
					FormLabelId = new Guid(),
					FormTemplateId = new Guid(),
					Index = 1,
					Value = "someValue"
				});
				context.FormInputs.Add(new CharSheet.Domain.FormInput
				{
					FormInputId = new Guid(),
					FormInputGroupId = new Guid(),
					Index = 1,
					Value = "someValue"
				});

				context.SaveChanges();

				context.FormTemplates.Add(new CharSheet.Domain.FormTemplate
				{
					FormTemplateId = new Guid(),
					TemplateId = new Guid(),
					FormPositionId = context.FormPositions.Where(f => f.FormTemplateId == templateId).Select(i => i.FormPostionId).FirstOrDefault(),
					FormPosition = context.FormPositions.Where(f => f.FormTemplateId == templateId).FirstOrDefault(),
					Type = "Type",
					Title = "Title",
					FormLabels = context.FormLabels.ToList()
				});

				context.SaveChanges();

				context.FormInputGroups.Add(new CharSheet.Domain.FormInputGroup
				{
					FormInputGroupId = new Guid(),
					SheetId = new Guid(),
					FormTemplateId = context.FormTemplates.Where(c => c.TemplateId == templateId).Select(c => c.TemplateId).FirstOrDefault(),
					FormTemplate = context.FormTemplates.Where(c => c.TemplateId == templateId).FirstOrDefault(),
					FormInputs = context.FormInputs.ToList()
				}) ;

				context.SaveChanges();

				context.Sheets.Add(new CharSheet.Domain.Sheet
				{
					SheetId = new Guid(),
					UserId = new Guid(),
					FormInputGroups = context.FormInputGroups.ToList()
				});

				context.SaveChanges();

			}

			//  Assert
			using (var context = new CharSheetContext(options))
			{
				var sheets = context.Sheets.ToList();
				var formInputGroups = context.FormInputGroups.ToList();
				Assert.Single(sheets);
				sheets[0].SheetId.Equals(new Guid());
				sheets[0].UserId.Equals(new Guid());
				Assert.Equal(formInputGroups, sheets[0].FormInputGroups);

			}
		}

		[Fact]
		public void AddTemplateWithDbContext()
		{
			//  Arrange
			var options = new DbContextOptionsBuilder<CharSheetContext>()
				.UseInMemoryDatabase(databaseName: "AddTemplate")
				.Options;

			Guid formId = new Guid();
			Guid templateId = new Guid();
			//  Act
			using (var context = new CharSheetContext(options))
			{

				//  First add Form position then add Form template
				context.FormPositions.Add(new CharSheet.Domain.FormPosition
				{
					FormPostionId = formId,
					FormTemplateId = templateId,
					OffsetTop = 20,
					OffsetLeft = 30,
					XPos = 80,
					YPos = 90,
					Width = 300,
					Height = 500
				});

				context.FormLabels.Add(new CharSheet.FormLabel
				{
					FormLabelId = new Guid(),
					FormTemplateId = new Guid(),
					Index = 1,
					Value = "someValue"
				});

				context.SaveChanges();

				context.FormTemplates.Add(new CharSheet.Domain.FormTemplate
				{
					FormTemplateId = new Guid(),
					TemplateId = new Guid(),
					FormPositionId = context.FormPositions.Where(f => f.FormTemplateId == templateId).Select(i => i.FormPostionId).FirstOrDefault(),
					FormPosition = context.FormPositions.Where(f => f.FormTemplateId == templateId).FirstOrDefault(),
					Type = "Type",
					Title = "Title",
					FormLabels = context.FormLabels.ToList()
				});

				context.SaveChanges();

				context.Templates.Add(new CharSheet.Domain.Template
				{
					TemplateId = new Guid(),
					UserId = new Guid(),
					FormTemplates = context.FormTemplates.ToList()
				});

				context.SaveChanges();
			}

			//  Assert
			using (var context = new CharSheetContext(options))
			{
				var templates = context.Templates.ToList();
				var formTemplates = context.FormTemplates.ToList();
				Assert.Single(templates);
				templates[0].TemplateId.Equals(new Guid());
				templates[0].UserId.Equals(new Guid());
				Assert.Equal(formTemplates, templates[0].FormTemplates);

			}

		}
	}
}
