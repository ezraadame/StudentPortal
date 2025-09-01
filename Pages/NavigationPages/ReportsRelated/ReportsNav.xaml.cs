using StudentPortal.Models;
using StudentPortal.Services;
using System.Collections.ObjectModel;

namespace StudentPortal.Pages.NavigationPages.ReportsRelated;

public partial class ReportsNav : ContentPage
{
    public ObservableCollection<Reports> ReportData { get; set; } = [];
    public ReportsNav()
    {
        InitializeComponent();
        ReportDataView.ItemsSource = ReportData;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await UpdateTimestamp();

        await ShowEmptyState();
    }

    private async Task UpdateTimestamp()
    {
        ReportTimestampLabel.Text = $"Generated on: {DateTime.Now:dddd, MMMM dd, yyyy 'at' h:mm:ss tt}";
    }
    private async void GenerateReportButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            ShowLoading(true);

            await LoadReportData();

            await UpdateTimestamp();

            ShowLoading(false);

            if (ReportData.Count > 0)
            {
                ShowReportData();
            }
            else
            {
                await ShowEmptyState();
            }
        }
        catch (Exception ex)
        {
            ShowLoading(false);
            await DisplayAlert("Error", $"Failed to generate report: {ex.Message}", "OK");
        }
    }
    private async Task LoadReportData()
    {
        try
        {
            ReportData.Clear();

            var courses = await DBService.GetCoursesByUser(UserSession.CurrentUserId);
            int totalAssessments = 0;

            foreach (var course in courses)
            {
                var assessments = await DBService.GetAssessmentsByCourseAndUser(course.Id, UserSession.CurrentUserId);

                foreach (var assessment in assessments)
                {
                    ReportData.Add(new Reports
                    {
                        CourseName = course.Name ?? "Unknown Course",
                        AssessmentName = assessment.Name ?? "Unknown Assessment",
                        Type = assessment.Type ?? "Unknown",
                        StartDate = assessment.StartDate,
                        EndDate = assessment.EndDate,
                    });
                    totalAssessments++;
                }
            }

            UpdateSummaryStats(courses.Count, totalAssessments);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error loading report data: {ex.Message}");
        }
    }
    private void UpdateSummaryStats(int totalCourses, int totalAssessments)
    {
        TotalCoursesLabel.Text = totalCourses.ToString();
        TotalAssessmentsLabel.Text = totalAssessments.ToString();
    }
    private void ShowLoading(bool isLoading)
    {
        LoadingIndicator.IsVisible = isLoading;
        LoadingIndicator.IsRunning = isLoading;

        GenerateReportButton.IsEnabled = !isLoading;
        RefreshReportButton.IsEnabled = !isLoading;
    }
    private void ShowReportData()
    {
        SummaryFrame.IsVisible = true;
        TableHeaderFrame.IsVisible = true;
        ReportDataView.IsVisible = true;

        EmptyStateFrame.IsVisible = false;
    }
    private async Task ShowEmptyState()
    {
        SummaryFrame.IsVisible = false;
        TableHeaderFrame.IsVisible = false;
        ReportDataView.IsVisible = false;

        EmptyStateFrame.IsVisible = true;
    }

    private async void RefreshReportButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            ShowLoading(true);

            await LoadReportData();

            await UpdateTimestamp();

            ShowLoading(false);

            if (ReportData.Count > 0)
            {
                ShowReportData();
            }
            else
            {
                await ShowEmptyState();
            }
        }
        catch (Exception ex)
        {
            ShowLoading(false);
            await DisplayAlert("Error", $"Failed to generate report: {ex.Message}", "OK");
        }
    }
}



