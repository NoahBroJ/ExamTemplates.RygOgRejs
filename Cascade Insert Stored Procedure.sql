/*  Author: Mads Mikkel Rasmussen. NOTE: @ means variable in SQL'sk.
 *
 * Imagine we have three tables: Employee, ContactInfos and Addresses. An employee has a contact
 * info and an address. But we can't insert into all three tables simultaniously, when we have 
 * auto incremented primary keys on each table. The solution is to use a stored procedure.
 * This stored procedure can do only one thing: Insert three new rows; 
 *	- one in the Employees table,
 *  - and one in the ContactInfos table with the correct foreign key to the row that was inserted 
 *	  in the Employees table, 
 *  - and one in the Addresses table with the correct foreign key to the row that was inserted in 
 *    the Employees table. 
 * The procedure gets 10 arguments that are saved in the parameters. In the following 'the first
 * table' is the table in your business logic that is the Employee table here. In your business 
 * it could perhaps be Journey or something... Here is each part explained:
 *
 *
 * Part A: Creating the procedure, and declaring what parameters it has. Each parameter has an 
 * identifier and a datatype. You should change the parameter list to accomodate your needs.
 *
 * Part B: Two variables are declared. The first is a so-called 'table' variable. It is declared
 * with a single column called Id with datatype INT. The other variable is an INT variable, just 
 * like in ordinary programming languages. You should rename the employeeId to reflect the name
 * of your primary key in the first table.
 * 
 * Part C: The INSERT statement also outputs the new value for the primary key, and stores it in
 * the temporary table variable's first row.
 * 
 * Part D: Simplye get the value for the primary key for the newly insterted employee row, and
 * save it to a variable.
 *
 * Part E: Use the value of the variable as the value for the foreign keys.                      */

-- Part A:
CREATE PROCEDURE CascadeInsert	-- Name of the procedure. 
	@Destination NVARCHAR(40),	-- a parameter to be inserted in Journeys table.
	@DepartureTime DATETIME2,	-- a parameter to be inserted in Journeys table.
	@Adults INT,				-- a parameter to be inserted in Journeys table.
	@Children INT,				-- a parameter to be inserted in Journeys table.
	@IsFirstClass BIT,			-- a parameter to be inserted in Journeys table.
	@LuggageAmount DECIMAL,		-- a parameter to be inserted in Journeys table.
	@FirstName NVARCHAR(20),	-- a parameter to be inserted in Payers table.
	@LastName NVARCHAR(20),		-- a parameter to be inserted in Payers table.
	@Ssn NVARCHAR(11),			-- a parameter to be inserted in Payers table.
	@Amount DECIMAL				-- a parameter to be inserted in Transactions table.
AS								-- just leave this

-- Part B:
DECLARE @tempIdTable TABLE (JId INT, PId INT)		-- just leave this.
DECLARE @JourneyId INT	-- just rename @employeeId to the name of your first table id column.
DECLARE @PayerId INT

-- Part C:
INSERT INTO Journeys (Destination, DepartureTime, Adults, Children, IsFirstClass, LuggageAmount)	-- change this accordingly.
	OUTPUT inserted.JourneyId INTO @tempIdTable(JId)	-- change to inserted.whateverId
	VALUES(@Destination, @DepartureTime, @Adults, @Children, @IsFirstClass, @LuggageAmount)

-- Part D:
SELECT @JourneyId = FIRST_VALUE(JId) OVER(PARTITION BY JId ORDER BY JId) FROM @tempIdTable --nochange

-- Part E:
INSERT INTO Payers (FirstName, LastName, Ssn, JourneyId)
	OUTPUT inserted.PayerId INTO @tempIdTable(PId)
	VALUES(@FirstName, @LastName, @Ssn, @JourneyId)

SELECT @PayerId = FIRST_VALUE(PId) OVER(PARTITION BY PId ORDER BY PId) FROM @tempIdTable --nochange

INSERT INTO Transactions (Amount, PayerId, JourneyId)
	VALUES(@Amount, @PayerId, @JourneyId)