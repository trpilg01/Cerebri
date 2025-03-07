import { useState } from "react";
import { requestCreateJournal } from "services";
import { IoClose } from "react-icons/io5";
import { CreateJournalRequest, Mood } from "data/dataTypes";
import { ErrorPopUp } from '..';
import { useMoods } from "hooks";

interface CheckInProps {
    setShowOverlay: (value: boolean) => void;
};

const todaysDate: string = new Date().toDateString();

const CreateJournal: React.FC<CheckInProps> = ({ setShowOverlay }) => {
    const [title, setTitle] = useState<string>(todaysDate);
    const [content, setContent] = useState<string>('');
    const [selectedMoods, setSelectedMoods] = useState<Mood[]>([]);
    const [error, setError] = useState<string | null>(null);
    const { moods } = useMoods();

    const handleCreate = async () => {
        if (title.length === 0 || title.length > 100) {
            setError("Test");
            return;
        }
        if (content.length === 0 || content.length > 1000) {
            alert("Invalid journal length.");
            return;
        }

        const request: CreateJournalRequest = {
            title: title,
            content: content,
            moods: selectedMoods
        };

        await requestCreateJournal(request);
        setShowOverlay(false);
    };

    const handleSelectedMoodsChange = (mood: Mood) => {
        setSelectedMoods((prevSelectedMoods) => {
            if (prevSelectedMoods.includes(mood)) {
                return prevSelectedMoods.filter((item) => item !== mood);
            }

            return [...prevSelectedMoods, mood];
        })
    }

    return (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
            <div className="flex flex-row w-11/12 h-full bg-astra p-10 rounded-sm">
                
                {/* Exit button */}
                <div className="fixed top-4 right-24" onClick={() => setShowOverlay(false)}>
                    <IoClose size={24} cursor={'pointer'} />
                </div>

                {/* Text Entry */}
                <div className="flex h-full w-2/3 bg-seaPink shadow-lg p-10 rounded-md justify-center">
                    <div className="flex flex-col h-full w-full items-center ">
                        <input
                            type="text"
                            value={title}
                            maxLength={100}
                            onChange={(e) => setTitle(e.target.value)}
                            className="text-center font-semibold text-2xl bg-transparent border-none rounded-sm mb-4 h-10 w-42 focus:outline-none focus:ring-2 focus:ring-blue-200"
                        />
                        <textarea
                            id="content"
                            value={content}
                            onChange={(e) => setContent(e.target.value)}
                            className="h-5/6 w-11/12 font-semibold rounded-sm shadow-md focus:outline-none p-4 focus:ring-2 focus:ring-blue-300"
                            maxLength={1000}
                        />
                    </div>
                </div>

                {/* Mood Menu */}
                <div className="flex flex-col h-full ml-2 w-1/3 p-10 rounded-md bg-blizzardBlue text-cneter items-center">
                    <h1 className="text-lg font-semibold">Selected Moods</h1>
                    
                    <div className="flex flex-row flex-wrap h-24 mt-5">
                    {selectedMoods.length === 0 ? <h1 className="font-semibold">No moods Selected</h1> : selectedMoods.map((mood) => (
                        <a className="m-1 font-semibold">{mood.name}</a>
                    ))}
                    </div>

                    <div className="flex-col h-full text-center justify-center overflow-y-scroll scrollbar-none">

                        <h1 className="text-lg mb-3 font-bold">Low Energy Positive</h1>
                        {moods && moods.lowEnergyPositive.map((mood) => (
                            <button
                                id={mood.id}
                                onClick={() => handleSelectedMoodsChange(mood)}
                                className={`h-fit w-fit bg-green-400 font-semibold p-1 m-1 rounded-lg cursor-pointer shadow-lg hover:opacity-55 ${selectedMoods.includes(mood) ? 'opacity-55' : ''}`}
                            >
                                {mood.name}
                            </button>   
                        ))}

                        <h1 className="text-lg mb-3 mt-3 font-bold">Low Energy Negative</h1>
                        {moods && moods.lowEnergyNegative.map((mood) => (
                            <button
                                id={mood.id}
                                onClick={() => handleSelectedMoodsChange(mood)}
                                className={`h-fit w-fit bg-blue-400 p-1 m-1 font-semibold rounded-lg cursor-pointer shadow-lg hover:opacity-55 ${selectedMoods.includes(mood) ? 'opacity-55' : ''}`}                            >
                                {mood.name}
                            </button>   
                        ))}

                        <h1 className="text-lg mb-3 mt-3 font-bold">High Energy Positive</h1>
                        {moods && moods.highEnergyPositive.map((mood) => (
                            <button
                                id={mood.id}
                                onClick={() => handleSelectedMoodsChange(mood)}
                                className={`h-fit w-fit bg-yellow-400 p-1 m-1 font-semibold rounded-lg cursor-pointer shadow-lg hover:opacity-55 ${selectedMoods.includes(mood) ? 'opacity-55' : ''}`}                            >
                                {mood.name}
                            </button>   
                        ))}

                        <h1 className="text-lg mb-4 mt-3 font-bold">High Energy Negative</h1>
                        {moods && moods.highEnergyNegative.map((mood) => (
                            <button
                                id={mood.id}
                                onClick={() => handleSelectedMoodsChange(mood)}
                                className={`h-fit w-fit bg-red-400 p-1 m-1 font-semibold rounded-lg cursor-pointer shadow-lg hover:opacity-55 ${selectedMoods.includes(mood) ? 'opacity-55' : ''}`}
                            >
                                {mood.name}
                            </button>   
                        ))}
                    </div>
                    
                    {/* Create Button */}
                    <button
                        onClick={handleCreate}
                        className="h-fit mt-2 w-fit p-2 rounded-lg bg-seaPink font-bold text-black text-lg cursor-pointer
                                   hover:opacity-55"
                    >
                        Create
                    </button>
                </div>
                {error && <ErrorPopUp message={error} setError={setError} />}
            </div>
        </div>
    );
};

export default CreateJournal;