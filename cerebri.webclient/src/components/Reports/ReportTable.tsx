import ReportRow from "components/reports/ReportRow";
import { FaPlus } from "react-icons/fa6";
import { ReportData } from "data/dataTypes";

interface ReportTableProps {
    reports: ReportData[] | undefined;
    onView: (id: string) => void;
    onDelete: (report: ReportData) => void;
    onUpdate: (report: ReportData) => void;
    onCreate: (value: boolean) => void;
};

const ReportTable:React.FC<ReportTableProps> = ({ reports, onView, onDelete, onUpdate, onCreate }) => (
    <table className="flex flex-col h-5/6 w-5/6 p-5 mt-10 overflow-scroll scrollbar-none bg-blizzardBlue rounded-md">
        <thead className="flex justify-evenly m-2">
            <th className="text-center w-1/3 uppercase">Date</th>
            <th className="text-center w-1/3 uppercase">Report Name</th>
            <th className="flex justify-center w-1/3">
                <button
                    onClick={() => onCreate(true)} 
                    className="flex flex-row items-center gap-2 bg-bittersweet rounded-md p-1 border-none"
                >
                    Create
                    <FaPlus />
                </button>
            </th>
        </thead>
        <tbody className="w-full">
            {reports?.map((report) => (
                <ReportRow
                    key={report.id}
                    report={report}
                    onView={onView}
                    onUpdate={onUpdate}
                    onDelete={onDelete}
                />
            ))}
        </tbody>
    </table>
)

export default ReportTable;