﻿using Cerebri.Application.Interfaces;
using Cerebri.Domain.Entities;
using DinkToPdf;
using DinkToPdf.Contracts;

namespace Cerebri.Infrastructure.ReportGeneration
{
    public class ReportGenerator : IReportGenerator
    {
        private readonly IConverter _converter;
        private readonly IOpenAIConnector _openAIConnector;
        private readonly IReportRepository _reportRepository;

        public ReportGenerator(IConverter converter, IOpenAIConnector connector, IReportRepository repository)
        {
            _converter = converter;
            _openAIConnector = connector;
            _reportRepository = repository;
        }

        public async Task<ReportModel> GenerateReport(List<JournalEntryModel> journals, Guid userId, MoodModel 
            mostCommonMood, string reportName = "New Report")
        {
            var reportInfo = await GenerateReportInfo(journals);

            var pdfDocument = new HtmlToPdfDocument
            {
                GlobalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4
                }
            };

            string htmlStyles = @"
                body {
                    font-family: Arial, sans-serif;
                    margin: 20px;
                }
                .header {
                    text-align: center;
                }
                .content {
                    margin-top: 20px;
                }
                .content h2 {
                    text-align: left;
                }
                .image {
                    text-align: center;
                }
                img {
                    max-width: 100%;
                    height: auto;
                }
            ";

            pdfDocument.Objects.Add(new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = $@"
                    <html>
                        <head>
                            <style>
                               {htmlStyles} 
                            </style>
                        </head>
                        <body>
                            <div class='header'>
                                <h1>{reportName}</h1>
                            </div>
                            <div class='content'>
                                <h2>Most Common Mood: {mostCommonMood.Name}</h2>
                                <h2>Summary:</h2>
                                <p>{reportInfo?.Summary ?? "Error"}</p>
                                <h2>Suggested Talking Points:</h2>
                                <p>{reportInfo?.Insights ?? "Error"}</p>
                            </div>
                        </body>
                    </html>",
                WebSettings = { DefaultEncoding = "utf-8" },
                HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontSize = 9, Center = "Generated by DinkToPdf" }
            });

            var reportData = _converter.Convert(pdfDocument);
            ReportModel report = new ReportModel
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                ReportName = reportName,
                ReportData = reportData,
                CreatedAt = DateTime.UtcNow
            };
            await _reportRepository.InsertAsync(report);
            return report;
        }

        public async Task<OpenAIResponseModel?> GenerateReportInfo(List<JournalEntryModel> journals)
        {
            if (journals != null)
            {
                var response = await _openAIConnector.Prompt(journals);
                return response;
            }
            return null;
        }
    }
}
