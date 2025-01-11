import { ReportData } from "data/dataTypes";
import { FaEye, FaTrash } from "react-icons/fa";
import { MdEdit } from "react-icons/md";
import { getDateString } from "services";

interface ReportRowProps {
    report: ReportData;
    onView: (id: string) => void;
    onDelete: (report: ReportData) => void;
    onUpdate: (report: ReportData) => void;
};

const ReportRow = ({ report, onView, onDelete, onUpdate }: ReportRowProps) => (
    <tr className="flex max-h-11 w-full">
        <td className="w-1/3 text-center">{getDateString(report.createdAt)}</td>
        <td className="w-1/3 text-center">{report.reportName}</td>
        <td className="flex flex-row w-1/3 items-center justify-center gap-2">
            <FaEye onClick={() => onView(report.id)} cursor={'pointer'} />
            <FaTrash onClick={() => onDelete(report)} cursor={'pointer'} />
            <MdEdit onClick={() => onUpdate(report)} cursor={'pointer'} />
        </td>
    </tr>
);

export default ReportRow;