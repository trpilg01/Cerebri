import { ReportData } from "data/dataTypes";
import { useEffect, useState } from "react";
import { FaTrash } from "react-icons/fa";
import { FaEye } from "react-icons/fa";
import { getDateString, requestReports, requestReportStream } from "services";

const MoodReports = () => {
    const [reports, setReports] = useState<ReportData[] | undefined>(undefined);

    const handleRequestPdf = async (reportId: string) => {
        try {
            const pdfBlob = await requestReportStream(reportId);
            const pdfUrl = URL.createObjectURL(pdfBlob);
            window.open(pdfUrl, '_blank');
        } catch (error: any) {
            console.error('Error fetching the pdf:', error);
        }
    };

    useEffect(() => {
        const fetchReports = async () => {
            const response = await requestReports();
            setReports(response);
            console.log("reports", reports);
        };

        fetchReports();
    }, []);

    return (
        <div className="flex flex-col text-center p-2 bg-pearlLusta h-full w-full">
            <h1 className="text-3xl font-semibold">Reports</h1>
            <div className="h-[90%] w-full p-4">
                <table className="h-fit w-[95%] bg-blizzardBlue rounded-md">
                    <thead className="w-full">
                        <th className="w-1/6 text-left">Date</th>
                        <th className="w-4/6 text-left">Report name</th>
                    </thead>
                    <tbody>
                        {reports && reports.map((report) => (
                            <tr key={report.id} className="max-h-8">
                                <td className="max-h-8 w-1/6">{getDateString(report.createdAt)}</td>
                                <td className="max-h-8 w-3/6">{report.reportName}</td>
                                <td className="max-h-8 w-1/8">
                                    <div className="flex h-full w-full justify-center">
                                        <FaEye cursor={'pointer'} onClick={() => handleRequestPdf(report.id)}/>
                                    </div>
                                </td>
                                <td className="max-h-8 w-1/8">
                                    <div className="flex h-full w-full justify-center">
                                        <FaTrash cursor={'pointer'}/>
                                    </div>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
};

export default MoodReports;