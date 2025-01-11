import { CheckInRow } from "..";
import { CheckIn } from "data/dataTypes";
import { FaPlus } from "react-icons/fa";

interface CheckInsTableProps {
    checkIns: CheckIn[] | undefined;
    onDelete: (id: string) => void;
    showCreate: (value: boolean) => void;
};

const CheckInsTable:React.FC<CheckInsTableProps> = ({ checkIns, onDelete, showCreate: onCreate }) => (
    <table className="flex flex-col h-5/6 w-5/6 p-5 mt-10 overflow-scroll scrollbar-none bg-blizzardBlue rounded-md">
        <thead className="flex justify-evenly m-2">
            <th className="text-center w-1/4 uppercase">Date</th>
            <th className="text-center w-1/4 uppercase">Moods</th>
            <th className="text-center w-1/4 uppercase">Notes</th>
            <th className="flex justify-center w-1/4">
                <button
                    className="flex flex-row items-center gap-2 bg-bittersweet rounded-md p-1 border-none"
                    onClick={() => onCreate(true)}
                >
                    Check In
                    <FaPlus />
                </button>
            </th>
        </thead>
        <tbody className="w-full">
            {checkIns?.map((checkIn) => (
                <CheckInRow
                    checkIn={checkIn}
                    onDelete={onDelete}
                />
            ))}
        </tbody>
    </table>
);

export default CheckInsTable;