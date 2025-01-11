import { CheckIn } from "data/dataTypes";
import { FaTrash } from "react-icons/fa";
import { getColor, getDateString } from "services";

interface CheckInRowProps {
    checkIn: CheckIn;
    onDelete: (id: string) => void;
};

const CheckInRow:React.FC<CheckInRowProps> = ({ checkIn, onDelete }) => (
    <tr className="flex max-h-11 w-full p-2 overflow-scroll scrollbar-none">
        <td className="max-h-11 w-1/4 text-center">{getDateString(checkIn.createdAt)}</td>
        <td className="max-h-11 w-1/4 flex flex-row items-center justify-center gap-2">
            {checkIn.moods.map((mood) => (
                <div
                    className={`p-1 font-semibold text-sm shadow-md rounded-md ${getColor(mood.type)}`}
                >
                    {mood.name}
                </div>
            ))}
        </td>
        <td className="max-h-11 text-center w-1/4 p-2">{checkIn.content}</td>
        <td className="flex flex-row w-1/4 items-center justify-center gap-2">
            <FaTrash 
                cursor={'pointer'}
                onClick={() => onDelete(checkIn.id)}
            />
        </td>
    </tr>
);

export default CheckInRow;