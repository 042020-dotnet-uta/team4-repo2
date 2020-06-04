using System;
using System.Linq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using CharSheet.Data;
using CharSheet.Domain;

namespace CharSheet.Test
{
    public class DbContextAddition : DbContextTests
    {
        [Fact]
        public void AddUserWithDbContext()
        {
            //  Arrange
            var options = GetOptions("AddUser");

            //  Act
            using (var context = GetContext(options))
            {
                context.Users.Add(new User());
                context.SaveChanges();
            }

            //  Assert
            using (var context = GetContext(options))
            {
                var users = context.Users.ToList();
                Assert.Single(users);
            }

        }

        [Fact]
        public void AddLoginWithDbContext()
        {
            // Arrange
            var options = GetOptions("AddLogin");

            // Act
            using (var context = GetContext(options))
            {
                context.Users.Add(new User
                {
					Login = new Login()
                });
				context.SaveChanges();
            }

			// Assert
			using (var context = GetContext(options))
			{
				var logins = context.Logins.ToList();
				Assert.Single(logins);
			}
        }

		[Fact]
		public void AddTemplateWithDbContext()
		{
			// Arrange
			var options = GetOptions("AddTemplate");

			// Act
			using (var context = GetContext(options))
			{
				context.Templates.Add(new Template());
				context.SaveChanges();
			}

			// Assert
			using (var context = GetContext(options))
			{
				var templates = context.Templates.ToList();
				Assert.Single(templates);
			}
		}

		[Fact]
		public void AddSheetWithDbContext()
		{
			// Arrange
			var options = GetOptions("AddSheet");

			// Act
			using (var context = GetContext(options))
			{
				context.Sheets.Add(new Sheet());
				context.SaveChanges();
			}

			// Assert
			using (var context = GetContext(options))
			{
				var sheets = context.Sheets.ToList();
				Assert.Single(sheets);
			}
		}

		[Fact]
		public void AddFormInputGroupWithDbContext()
		{
			// Arrange
			var options = GetOptions("AddFormInputGroup");

			// Act
			using (var context = GetContext(options))
			{
				context.FormInputGroups.Add(new FormInputGroup());
				context.SaveChanges();
			}

			// Assert
			using (var context = GetContext(options))
			{
				var formInputGroups = context.FormInputGroups.ToList();
				Assert.Single(formInputGroups);
			}
		}

		[Fact]
		public void AddFormTemplateWithDbContext()
		{
			// Arrange
			var options = GetOptions("AddFormTemplate");

			// Act
			using (var context = GetContext(options))
			{
				context.FormTemplates.Add(new FormTemplate());
				context.SaveChanges();
			}

			// Assert
			using (var context = GetContext(options))
			{
				var formTemplates = context.FormTemplates.ToList();
				Assert.Single(formTemplates);
			}
		}

		[Fact]
		public void AddFormInputWithDbContext()
		{
			// Arrange
			var options = GetOptions("AddFormInput");

			// Act
			using (var context = GetContext(options))
			{
				context.FormInputs.Add(new FormInput());
				context.SaveChanges();
			}

			// Assert
			using (var context = GetContext(options))
			{
				var formInputs = context.FormInputs.ToList();
				Assert.Single(formInputs);
			}
		}

		[Fact]
		public void AddFormPositionWithDbContext()
		{
			// Arrange
			var options = GetOptions("AddFromPosition");

			// Act
			using (var context = GetContext(options))
			{
				context.FormPositions.Add(new FormPosition());
				context.SaveChanges();
			}

			// Assert
			using (var context = GetContext(options))
			{
				var formPositions = context.FormPositions.ToList();
				Assert.Single(formPositions);
			}
		}

		[Fact]
		public void AddFormLabelWithDbContext()
		{
			// Arrange
			var options = GetOptions("AddFormLabel");

			// Act
			using (var context = GetContext(options))
			{
				context.FormLabels.Add(new FormLabel());
				context.SaveChanges();
			}
			
			// Assert
			using (var context = GetContext(options))
			{
				var formLabels = context.FormLabels.ToList();
				Assert.Single(formLabels);
			}
		}

		[Fact]
		public void CreateFormLabel()
		{
			// Arrange
			var options = GetOptions("CreateFormLabel");

			// Act
			using (var context = GetContext(options))
			{
				context.FormLabels.Add(new FormLabel
				{
					FormLabelId = new Guid(),
					FormTemplateId = new Guid(),
					Index = 1,
					Value = "someValue"
				}) ;
				context.SaveChanges();
			}

			// Assert
			using (var context = GetContext(options))
			{
				var formLabels = context.FormLabels.ToList();
				var label = formLabels.FirstOrDefault();
				Assert.Equal("someValue", label.Value);
			}
		}

		[Fact]
		public void CreateFormInput()
		{
			// Arrange
			var options = GetOptions("CreateFormInput");

			// Act
			using (var context = GetContext(options))
			{
				context.FormInputs.Add(new FormInput
				{
					FormInputGroupId = new Guid(),
					FormInputId = new Guid(),
					Value = "someValue",
					Index = 1
				}) ;
				context.SaveChanges();
			}

			// Assert
			using (var context = GetContext(options))
			{
				var formInputs = context.FormInputs.ToList();
				var label = formInputs.FirstOrDefault();
				Assert.Equal("someValue", label.Value);
			}
		}
	}
}
