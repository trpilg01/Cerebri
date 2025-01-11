import { useState } from "react";
import { UpdateReport, CreateReportMenu, ReportTable } from "..";
import { ReportData } from "data/dataTypes";
import { useReports } from 'hooks';

const Reports = () => {
    const { reports, deleteReport, viewReportPdf} = useReports();
    const [selectedReport, setSelectedReport] = useState<ReportData| null>(null);
    const [showCreateMenu, setShowCreateMenu] = useState<boolean>(false);

    const handleUpdate = (report: ReportData) => {
        setSelectedReport(report);
    };
    
    return (
        <div className="h-full w-full flex flex-col justify-center items-center bg-pearlLusta">
            <h1 className="text-2xl font-bold">Reports</h1>
            <ReportTable
                reports={reports}
                onView={viewReportPdf}
                onDelete={deleteReport}
                onUpdate={handleUpdate}
                onCreate={setShowCreateMenu}
            />
            {selectedReport && <UpdateReport setSelectedReport={setSelectedReport} report={selectedReport}/>}
            {showCreateMenu && <CreateReportMenu setShowMenu={setShowCreateMenu}/>}
        </div>
    );
};

export default Reports;