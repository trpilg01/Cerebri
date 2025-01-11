import { ReportData, UpdatedReport } from "data/dataTypes";
import { useState } from "react";
import { IoMdCheckmark, IoMdClose } from "react-icons/io";
import { requestReportUpdate } from "services";

interface UpdateReportProps {
    setSelectedReport: (value: ReportData | null) => void; 
    report: ReportData | null
};

const UpdateReport:React.FC<UpdateReportProps> = ({ setSelectedReport, report }) => {
    const [reportName, setReportName] = useState<string>(report?.reportName || "");
    const [error, setError] = useState<string | null>(null);
    const handleCancel = () => setSelectedReport(null);

    const handleUpdateRequest = async () => {
        setError(null);
        if (!reportName.trim()) {
            setError("Report name cannot be empty");
            return;
        }

        const updatedReport: UpdatedReport = {
            id: report?.id,
            reportName,
            createdAt: report?.createdAt || new Date()
        };
        try {
            await requestReportUpdate(updatedReport);
            setSelectedReport(null);
        } catch (err: any) {
            setError(err.message || "Failed to update the report");
        }
    };

    return (
        <div className="fixed flex flex-col inset-0 bg-black bg-opacity-50 items-center justify-center z-50">
            <div className="flex flex-row h-fit p-5 w-fit bg-blizzardBlue rounded-md justify-center items-center gap-2">
                <a className="font-semibold">Report Name</a>
                <input
                    value={reportName}
                    onChange={(e) => setReportName(e.target.value)}
                    className="border-none rounded-sm"
                    maxLength={50}
                />
                <IoMdCheckmark size={24} cursor={'pointer'} onClick={handleUpdateRequest}/>
                <IoMdClose size={24} cursor={'pointer'} onClick={handleCancel}/>
            </div>
            {error && <h1 className="text-xl text-red-500 font-bold">Error: {error}</h1>}
        </div>
    );
};

export default UpdateReport;