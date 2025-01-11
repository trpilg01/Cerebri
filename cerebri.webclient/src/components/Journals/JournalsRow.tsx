import { Journal } from "data/dataTypes";
import { FaTrash, FaEye } from "react-icons/fa";
import { getColor, getDateString } from "services";

interface JournalsRowProps {
    journal: Journal;
    onDelete: (journalId: string) => void;
    onView: (journal: Journal) => void;
};

const JournalsRow: React.FC<JournalsRowProps> = ({ journal, onDelete, onView}) => (
    <tr className="flex max-h-11 w-full p-2">
        <td className="w-1/4 text-center font-semibold">{getDateString(journal.createdAt)}</td>
        <td className="w-1/4 text-center">{journal.title}</td>
        <td className="w-1/4 flex flex-row items-center justify-center gap-2">
            {journal.moods.map((mood) => (
                <div className={`p-1 font-semibold shadow-md text-sm ${getColor(mood.type)} rounded-md`}>
                    {mood.name}
                </div>
            ))}
        </td>
        <td className="flex flex-row w-1/4 items-center justify-center gap-2">
            <FaEye
                onClick={() => onView(journal)} 
                cursor={'pointer'}
            />
            <FaTrash 
                onClick={() => onDelete(journal.id)}
                cursor={'pointer'} 
            />
        </td>
    </tr>
)

export default JournalsRow;