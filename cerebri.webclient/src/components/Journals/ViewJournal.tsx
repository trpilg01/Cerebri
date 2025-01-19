import { Journal } from "data/dataTypes";
import { IoMdClose } from "react-icons/io";
import { getMoodColor } from "services";

interface ViewJournalProps {
    journal: Journal | null;
    setSelectedJournal: (value: Journal | null) => void;
}

const ViewJournal: React.FC<ViewJournalProps> = ({ journal, setSelectedJournal }) => {
    return (
        <div className="fixed flex-col inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
            <div className="h-[95%] p-3 flex flex-col rounded-md w-1/2 bg-pearlLusta just-center text-center">
                <div className="flex justify-end mr-2">
                    <IoMdClose size={20} onClick={() => setSelectedJournal(null)} cursor={'pointer'}/>
                </div>
                <h1 className="mt-10 text-3xl">{journal?.title}</h1>
                <div className="flex flex-row h-fit w-full justify-center">
                    {journal?.moods.map((mood) => 

                            <button
                                className={`h-fit w-fit rounded-md p-2 my-4 text-sm mx-1 shadow-md ${getMoodColor(mood.type)}`}
                            >
                                {mood.name}
                            </button>
                    )}
                </div>
                <a className="text-left h-[90%] overflow-y-scroll scrollbar-none p-3 rounded-md bg-white shadow-md">{journal?.content}</a>
            </div>
        </div>
    );
};

export default ViewJournal;