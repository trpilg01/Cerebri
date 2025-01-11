import { JournalsRow } from "..";
import { Journal } from "data/dataTypes";

interface JournalsTableProps {
    journals: Journal[] | undefined;
    onDelete: (id: string) => void;
    onCreate: (value: boolean) => void;
    onView: (journal: Journal) => void; 
};

const JournalsTable:React.FC<JournalsTableProps> = ({ journals, onDelete, onCreate, onView }) => (
    <table className="flex flex-col h-5/6 w-5/6 p-5 mt-10 overflow-scroll scrollbar-none bg-blizzardBlue rounded-md">
        <thead className="flex justify-evenly m-2">
            <th className="text-center w-1/4 uppercase">Date</th>
            <th className="text-center w-1/4 uppercase">Title</th>
            <th className="text-center w-1/4 uppercase">Moods</th>
            <th className="flex justify-center w-1/4">
                <button
                    onClick={() => onCreate(true)} 
                    className="flex flex-row items-center gap-2 bg-bittersweet rounded-md p-1 border-none"
                >
                    Create
                </button>
            </th>
        </thead>
        <tbody>
            {journals?.map((journal) => (
                <JournalsRow
                    journal={journal}
                    onDelete={onDelete}
                    onView={onView}
                />
            ))}
        </tbody>
    </table>
)

export default JournalsTable;