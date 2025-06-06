USE [GTH1]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 19-04-2025 22:34:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Designations]    Script Date: 19-04-2025 22:34:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Designations](
	[DesignationId] [int] IDENTITY(1,1) NOT NULL,
	[DesignationName] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[DesignationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeAttendance]    Script Date: 19-04-2025 22:34:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeAttendance](
	[Month] [int] NULL,
	[Year] [int] NULL,
	[EmployeeId] [int] NULL,
	[PresentDay] [int] NULL,
	[LOP] [int] NULL,
	[OverTime] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 19-04-2025 22:34:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EmployeeId] [int] IDENTITY(1,1) NOT NULL,
	[Designation] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[DOB] [date] NOT NULL,
	[HireDate] [date] NOT NULL,
	[Salary] [bigint] NOT NULL,
	[MobileNo] [nvarchar](10) NOT NULL,
	[AccountNo] [nvarchar](50) NOT NULL,
	[BankName] [nvarchar](100) NOT NULL,
	[IFSC] [nvarchar](20) NOT NULL,
	[PermanentAddress] [nvarchar](255) NOT NULL,
	[CurrentAddress] [nvarchar](255) NOT NULL,
	[Pan] [nvarchar](20) NOT NULL,
	[UAN] [nvarchar](50) NULL,
	[AadharNo] [bigint] NOT NULL,
	[Image] [varbinary](max) NULL,
	[UserName] [nvarchar](50) NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](255) NULL,
	[Role] [nvarchar](50) NOT NULL,
	[FileName] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RemeaningLeaves]    Script Date: 19-04-2025 22:34:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RemeaningLeaves](
	[EmployeeId] [int] NULL,
	[Month] [int] NULL,
	[Year] [int] NULL,
	[Reamaningleave] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalaryRecords]    Script Date: 19-04-2025 22:34:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalaryRecords](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[Name] [varchar](250) NULL,
	[BankName] [nvarchar](100) NULL,
	[AccountNo] [nvarchar](50) NULL,
	[HireDate] [datetime] NULL,
	[Email] [nvarchar](100) NULL,
	[PAN] [nvarchar](20) NULL,
	[MobileNo] [nvarchar](20) NULL,
	[UAN] [nvarchar](20) NULL,
	[Salary] [decimal](18, 2) NULL,
	[PresentDay] [int] NULL,
	[OverTime] [int] NULL,
	[LOP] [int] NULL,
	[Month] [int] NULL,
	[Year] [int] NULL,
	[ReamaningLeave] [int] NULL,
	[DesignationName] [nvarchar](100) NULL,
	[MonthlySalary] [decimal](18, 2) NULL,
	[ActualLOP] [decimal](18, 2) NULL,
	[CalculateSalary] [decimal](18, 2) NULL,
	[BasicSalary] [decimal](18, 2) NULL,
	[ActualGetSalary] [decimal](18, 2) NULL,
	[TotalDeduction] [decimal](18, 2) NULL,
	[HRA] [decimal](18, 2) NULL,
	[STAT] [decimal](18, 2) NULL,
	[ProfessionalAllowance] [decimal](18, 2) NULL,
	[CCA] [decimal](18, 2) NULL,
	[SpecialAllowance] [decimal](18, 2) NULL,
	[Total] [decimal](18, 2) NULL,
	[PF] [decimal](18, 2) NULL,
	[ProfessionalTax] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EmployeeAttendance]  WITH NOCHECK ADD FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Designations] FOREIGN KEY([Designation])
REFERENCES [dbo].[Designations] ([DesignationId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Designations]
GO
ALTER TABLE [dbo].[RemeaningLeaves]  WITH CHECK ADD FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
/****** Object:  StoredProcedure [dbo].[Sp_CalculateSalary]    Script Date: 19-04-2025 22:34:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Sp_CalculateSalary] 
    @month INT,
    @year INT
AS
BEGIN
    SET NOCOUNT ON;

    -- STEP 1: Temporary Table to hold salary and leave info
    IF OBJECT_ID('tempdb..#SalaryCalculation') IS NOT NULL
        DROP TABLE #SalaryCalculation;

    SELECT 
        e.EmployeeId,
        ea.LOP,
        ISNULL(rl.ReamaningLeave, 0) AS RemainingLeave
    INTO #SalaryCalculation
    FROM Employees e
    JOIN EmployeeAttendance ea ON e.EmployeeId = ea.EmployeeId
    LEFT JOIN RemeaningLeaves rl ON rl.EmployeeId = e.EmployeeId
    WHERE ea.Month = @month AND ea.Year = @year;

    -- STEP 2: Calculate Salary
    SELECT 
        e.EmployeeId,
        e.BankName,
		e.Name,
        e.AccountNo,
        e.HireDate,
        e.Email,
        e.PAN,
        e.MobileNo,
        e.UAN,
        e.Salary,
        ea.PresentDay,
        ea.OverTime,
        ea.LOP,
        ea.Month,
        ea.Year,
        ISNULL(rl.ReamaningLeave, 0) AS ReamaningLeave,
        d.DesignationName,

        (e.Salary / 12) AS MonthlySalary,

        CASE 
            WHEN (ea.LOP - ISNULL(rl.ReamaningLeave, 0)) > 0 THEN (ea.LOP - ISNULL(rl.ReamaningLeave, 0)) 
            ELSE 0 
        END AS ActualLOP,

       ((e.Salary / 12) - (((e.Salary / 12) / ea.PresentDay) * 
              CASE 
                WHEN (ea.LOP - ISNULL(rl.ReamaningLeave, 0)) > 0 THEN (ea.LOP - ISNULL(rl.ReamaningLeave, 0)) 
                ELSE 0 
              END)) AS CalculateSalary,

        ((((e.Salary / 12) / ea.PresentDay) * ea.PresentDay) / 2.83) AS BasicSalary,

     (((((e.Salary / 12) / ea.PresentDay) * ea.PresentDay) / 2.83) * 40 / 100) AS HRA,

       (((((e.Salary / ea.PresentDay) * ea.PresentDay) / 2.83) * 10 / 100)) AS STAT,

        (((((e.Salary / 12) / ea.PresentDay) * ea.PresentDay) / 2.83) * 41 / 100) AS ProfessionalAllowance,

     (((((e.Salary / 12) / ea.PresentDay) * ea.PresentDay) / 2.83) * 41 / 100) AS CCA,

       (
            ((e.Salary / 12) - (((e.Salary / 12) / ea.PresentDay) * ea.LOP))
            - (
                ((((e.Salary / 12) / ea.PresentDay) * ea.PresentDay) / 2.83)
                + ((((e.Salary / 12) / ea.PresentDay) * ea.PresentDay) / 2.83) * 40 / 100
                + ((((e.Salary / 12) / ea.PresentDay) * ea.PresentDay) / 2.83) * 10 / 100
                + ((((e.Salary / 12) / ea.PresentDay) * ea.PresentDay) / 2.83) * 41 / 100
                + ((((e.Salary / 12) / ea.PresentDay) * ea.PresentDay) / 2.83) * 51 / 100
                + ((((e.Salary / 12) / ea.PresentDay) * ea.PresentDay) / 2.83) * 41 / 100
            )
            + (((((e.Salary / 12) / ea.PresentDay) * ea.PresentDay) / 2.83) * 51 / 100)
        ) AS SpecialAllowance,

         (
            ((((e.Salary / 12) / ea.PresentDay) * ea.PresentDay) / 2.83)
            + ((((e.Salary / 12) / ea.PresentDay) * ea.PresentDay) / 2.83) * 40 / 100
            + ((((e.Salary / 12) / ea.PresentDay) * ea.PresentDay) / 2.83) * 10 / 100
            + ((((e.Salary / 12) / ea.PresentDay) * ea.PresentDay) / 2.83) * 41 / 100
            + ((((e.Salary / 12) / ea.PresentDay) * ea.PresentDay) / 2.83) * 51 / 100
            + ((((e.Salary / 12) / ea.PresentDay) * ea.PresentDay) / 2.83) * 41 / 100
        ) AS Total,

        (((e.Salary / 12) / 2.83) * 12 / 100) AS PF,

		-- CalculateTotal Deduction
		((((e.Salary / 12) / 2.83) * 12 / 100))+(CASE 
            WHEN @month < 12 THEN 200
            ELSE 300
        END) as TotalDeduction,

		-- Total Calculated Salary Minus Deduction
		(  (
            ((((e.Salary / 12) / ea.PresentDay) * ea.PresentDay) / 2.83)
            + ((((e.Salary / 12) / ea.PresentDay) * ea.PresentDay) / 2.83) * 40 / 100
            + ((((e.Salary / 12) / ea.PresentDay) * ea.PresentDay) / 2.83) * 10 / 100
            + ((((e.Salary / 12) / ea.PresentDay) * ea.PresentDay) / 2.83) * 41 / 100
            + ((((e.Salary / 12) / ea.PresentDay) * ea.PresentDay) / 2.83) * 51 / 100
            + ((((e.Salary / 12) / ea.PresentDay) * ea.PresentDay) / 2.83) * 41 / 100
        ))-(((((e.Salary / 12) / 2.83) * 12 / 100))+(CASE 
            WHEN @month < 12 THEN 200
            ELSE 300
        END)) as ActualGetSalary,

        CASE 
            WHEN @month < 12 THEN 200
            ELSE 300
        END AS ProfessionalTax

    FROM 
        Employees e
        JOIN EmployeeAttendance ea ON e.EmployeeId = ea.EmployeeId
        JOIN Designations d ON e.Designation = d.DesignationId
        LEFT JOIN RemeaningLeaves rl ON rl.EmployeeId = e.EmployeeId
    WHERE
        ea.Month = @month AND ea.Year = @year;

    -- STEP 3: Update Remaining Leaves AFTER Calculation
    UPDATE rl
    SET rl.ReamaningLeave = 
        CASE 
            WHEN rl.ReamaningLeave >= sc.LOP THEN rl.ReamaningLeave - sc.LOP
            ELSE 0
        END
    FROM RemeaningLeaves rl
    INNER JOIN #SalaryCalculation sc ON rl.EmployeeId = sc.EmployeeId;

    -- Clean up
    DROP TABLE #SalaryCalculation;
END
GO
