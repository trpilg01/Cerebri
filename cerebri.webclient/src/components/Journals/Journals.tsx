import { Journal } from "data/dataTypes";
import { useEffect, useState } from "react";
import { FaTrash } from "react-icons/fa";
import { FaEye } from "react-icons/fa";
import { getColor, requestDeleteJournal, requestJournals } from "services";
import { CheckIn, ViewJournal } from '..';

const getDateString = (date: Date) => {
    const validDate = new Date(date);
    return validDate.toLocaleDateString();
}


const Journals = () => {
    const [journals, setJournals] = useState<Journal[] | undefined>(undefined);
    const [showViewJournal, setShowViewJournal] = useState<boolean>(false);
    const [showCreateJournal, setShowCreateJournal] = useState<boolean>(false);
    const [selectedJournal, setSelectedJournal] = useState<Journal | null>(null);

    const handleDelete = async (entryId: string) => {
        const response = await requestDeleteJournal(entryId);

        const updatedJournals = journals?.filter((journal) => (
            journal.id != entryId
        ));
        setJournals(updatedJournals);
        console.log(response);
    } ;

    const handleViewJournal = (selectedJournal: Journal) => {
        setSelectedJournal(selectedJournal);
        setShowViewJournal(true);
    }

    useEffect(() => {
        const fetchJournals = async () => {
            const response = await requestJournals();
            setJournals(response);
        }

        fetchJournals();
    }, []);

    return(
        <div className="h-full w-full flex flex-col justify-center items-center bg-pearlLusta">
            <h1 className="text-2xl font-bold">Journals</h1>
            <div className="h-5/6 w-5/6 mt-10 overflow-y-scroll scrollbar-none">
                <table className="w-full bg-blizzardBlue rounded-md shadow-xl">
                    <thead>
                        <th className="px-6 py-3 text-center text-black uppercase tracking-wider">Date</th>
                        <th className="px-6 py-3 text-center text-black uppercase tracking-wider">Title</th>
                        <th className="px-6 py-3 text-center text-black uppercase tracking-wider">Moods</th>
                        <th>
                            <button
                                className="bg-bittersweet cursor-pointer p-1 rounded-md"
                                onClick={() => setShowCreateJournal(true)}
                            >
                                Create
                            </button>
                        </th>
                    </thead>
                    <tbody>
                        {journals?.map((item) => (
                            <tr style={{ "height" : "75px" }} key={item.id}>
                                <td className="text-center">{getDateString(item.createdAt)}</td>
                                <td className="text-center">{item.title}</td>
                                <td className="text-center">
                                    {item.moods.map((mood) => (
                                        <button
                                            key={mood.id}
                                            className={`h-fit p-1 text-sm mx-2 rounded-lg bg-bittersweet ${getColor(mood.type)} shadow-md`}
                                        >
                                            {mood.name}
                                        </button>
                                    ))}
                                </td>
                                <td className="items-center">
                                    <FaEye cursor={'pointer'} onClick={() => handleViewJournal(item)}/>
                                </td>
                                <td className="items-center">
                                    <FaTrash cursor={'pointer'} onClick={() => handleDelete(item.id)}/>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
            {showViewJournal && <ViewJournal setShowViewJournal={setShowViewJournal} journal={selectedJournal}/>}
            {showCreateJournal && <CheckIn setShowOverlay={setShowCreateJournal}/>}
        </div>
    );
};

export default Journals;