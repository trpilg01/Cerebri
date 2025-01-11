import { useState, useEffect } from "react";
import { ReportData, DeleteReport } from "data/dataTypes";
import { requestReports, requestReportDelete, requestReportStream } from 'services';

const useReports = () => {
    const [reports, setReports] = useState<ReportData[] | undefined>(undefined);
    const [error, setError] = useState<string | null>(null);

    const fetchReports = async () => {
        try {
            const response = await requestReports();
            setReports(response);
        } catch (err: any) {
            setError("Failed to fetch reports");
        }
    };

    const deleteReport = async (report: ReportData) => {
        const reportToDelete: DeleteReport = {
            reportId: report.id
        };
        try {
            await requestReportDelete(reportToDelete);
            await fetchReports();
        } catch (err: any) {
            setError("Failed to delete report");
        }
    };

    const viewReportPdf = async (reportId: string) => {
        try {
            const pdfBlob = await requestReportStream(reportId);
            const pdfUrl = URL.createObjectURL(pdfBlob);
            window.open(pdfUrl, "_blank");
        } catch {
            setError("Failed to fetch PDF");
        }
    };

    useEffect(() => {
        fetchReports();
    }, []);

    return { reports, fetchReports, deleteReport, viewReportPdf, error};
};

export default useReports;