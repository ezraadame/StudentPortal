using StudentPortal.Models;
using StudentPortal.Services;
using System.Collections.ObjectModel;

namespace StudentPortal.Pages.NavigationPages.ReportsRelated;

public partial class ReportsNav : ContentPage
{
    public ObservableCollection<Reports> ReportData { get; set; } = new();
    public ReportsNav()
	{
		InitializeComponent();
        ReportDataView.ItemsSource = ReportData;
    }

    /// <summary>
    /// This gets called when the page appears. Think of it like a librarian
    /// setting up the reading room before visitors arrive.
    /// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Show when the page was opened
        UpdateTimestamp();

        // Show the empty state initially
        ShowEmptyState();
    }

    /// <summary>
    /// Updates the timestamp to show when the report page was accessed.
    /// This is like putting a "last updated" sticker on a bulletin board.
    /// </summary>
    private void UpdateTimestamp()
    {
        ReportTimestampLabel.Text = $"Generated on: {DateTime.Now:dddd, MMMM dd, yyyy 'at' h:mm:ss tt}";
    }

    /// <summary>
    /// This is our main report generation method. Think of it like a factory
    /// that takes raw materials (database records) and produces a finished
    /// product (formatted report).
    /// </summary>
    private async void GenerateReportButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Show loading indicator - like putting up a "Please Wait" sign
            ShowLoading(true);

            // Generate the report data
            await LoadReportData();

            // Update timestamp to show when report was generated
            UpdateTimestamp();

            // Hide loading and show results
            ShowLoading(false);

            if (ReportData.Count > 0)
            {
                ShowReportData();
            }
            else
            {
                ShowEmptyState();
            }
        }
        catch (Exception ex)
        {
            ShowLoading(false);
            await DisplayAlert("Error", $"Failed to generate report: {ex.Message}", "OK");
        }
    }


    /// <summary>
    /// This method does the actual work of gathering data from the database
    /// and converting it into report format. Think of it like a data detective
    /// that goes through all the files and organizes them into a neat summary.
    /// </summary>
    private async Task LoadReportData()
    {
        try
        {
            // Clear any existing data first
            ReportData.Clear();

            // Get all courses from the database
            var courses = await DBService.GetCourses();

            // Counter for summary statistics
            int totalAssessments = 0;

            // Go through each course and get its assessments
            foreach (var course in courses)
            {
                var assessments = await DBService.GetAssessmentsByCourse(course.Id);

                // Convert each assessment into a report item
                foreach (var assessment in assessments)
                {
                    var reportItem = new Reports
                    {
                        CourseName = course.Name ?? "Unknown Course",
                        AssessmentName = assessment.Name ?? "Unknown Assessment",
                        Type = assessment.Type ?? "Unknown",
                        StartDate = assessment.StartDate,
                        EndDate = assessment.EndDate,
                        
                    };

                    ReportData.Add(reportItem);
                    totalAssessments++;
                }
            }

            // Update summary statistics
            UpdateSummaryStats(courses.Count, totalAssessments);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error loading report data: {ex.Message}");
        }
    }

    /// <summary>
    /// This method figures out if an assessment is upcoming, in progress, or completed
    /// based on the current date. Think of it like a traffic light system -
    /// it tells you the current status of each assessment.
    /// </summary>
    

    
    private void UpdateSummaryStats(int totalCourses, int totalAssessments)
    {
        TotalCoursesLabel.Text = totalCourses.ToString();
        TotalAssessmentsLabel.Text = totalAssessments.ToString();
    }

    /// <summary>
    /// Shows or hides the loading indicator.
    /// Think of this like a "Please Wait" spinner you see on websites.
    /// </summary>
    private void ShowLoading(bool isLoading)
    {
        LoadingIndicator.IsVisible = isLoading;
        LoadingIndicator.IsRunning = isLoading;

        // Disable buttons while loading to prevent multiple clicks
        GenerateReportButton.IsEnabled = !isLoading;
        RefreshReportButton.IsEnabled = !isLoading;
    }

    /// <summary>
    /// Shows the actual report data and hides other states.
    /// This is like opening the curtains to reveal the main show.
    /// </summary>
    private void ShowReportData()
    {
        // Show the report components
        SummaryFrame.IsVisible = true;
        TableHeaderFrame.IsVisible = true;
        ReportDataView.IsVisible = true;

        // Hide the empty state
        EmptyStateFrame.IsVisible = false;
    }

    /// <summary>
    /// Shows the empty state when there's no data to display.
    /// This is like putting up a "No Events Today" sign.
    /// </summary>
    private void ShowEmptyState()
    {
        // Hide the report components
        SummaryFrame.IsVisible = false;
        TableHeaderFrame.IsVisible = false;
        ReportDataView.IsVisible = false;

        // Show the empty state message
        EmptyStateFrame.IsVisible = true;
    }

    private async void RefreshReportButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Show loading indicator - like putting up a "Please Wait" sign
            ShowLoading(true);

            // Generate the report data
            await LoadReportData();

            // Update timestamp to show when report was generated
            UpdateTimestamp();

            // Hide loading and show results
            ShowLoading(false);

            if (ReportData.Count > 0)
            {
                ShowReportData();
            }
            else
            {
                ShowEmptyState();
            }
        }
        catch (Exception ex)
        {
            ShowLoading(false);
            await DisplayAlert("Error", $"Failed to generate report: {ex.Message}", "OK");
        }
    }
}



