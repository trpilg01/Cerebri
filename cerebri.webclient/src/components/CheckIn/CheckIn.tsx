import { CreateCheckIn, Mood, Moods } from "data/dataTypes";
import { useEffect, useState } from "react";
import { getColor, getMoods, requestCreateCheckIn } from "services";

interface CheckInProps {
    setShowOverlay: (value: boolean) => void;
}

const CheckIn:React.FC<CheckInProps> = ({ setShowOverlay }) => {
    const [moods, setMoods] = useState<Moods | null>(null);
    const [selectedMoods, setSelectedMoods] = useState<Mood[]>([]);
    const [notes, setNotes] = useState<string>('');
    
    const handleMoodSelection = (mood : Mood) => {
        setSelectedMoods((prevSelectedMoods) => {
            if (prevSelectedMoods.includes(mood)) {
                return prevSelectedMoods.filter((item) => item !== mood);
            }
            if (prevSelectedMoods.length === 3) {
                return prevSelectedMoods;
            }
            return [...prevSelectedMoods, mood];
        })
    };

    const handleSave = async () => {
        const request: CreateCheckIn = {
            content: notes,
            moods: selectedMoods
        }
        await requestCreateCheckIn(request);
        setShowOverlay(false);
    } 

    useEffect(() => {
        const fetchMoods = async () => {
            const response: Moods | null = await getMoods();
            setMoods(response);
        }

        fetchMoods();
    }, [])

    return (
        <div className="fixed flex flex-col inset-0 bg-black bg-opacity-50 items-center justify-center z-50">
            <div className="flex w-1/2 h-3/4 bg-astra p-10 rounded-md">
            <div className="flex flex-col h-full w-full">
                <div className="flex flex-row h-full w-full">
                    {/* Mood Menu */}
                    <div className="h-full p-2 w-2/3 mx-2 bg-slate-400 shadow-lg rounded-md overflow-y-scroll scrollbar-none">
                        <h1 className="font-semibold m-2">Low Energy Positive Moods</h1>
                        {moods && moods.lowEnergyPositive.map((mood) => (
                            <button
                                key={mood.id}
                                className={`h-fit w-fit p-2 ${getColor(mood.type)} ${selectedMoods.includes(mood) ? 'opacity-55' : '' } rounded-md m-1 text-sm font-semibold cursor-pointer`}
                                onClick={() => handleMoodSelection(mood)}
                            >
                                {mood.name}
                            </button>
                        ))}

                        <h1 className="font-semibold m-2">Low Energy Negative</h1>
                        {moods && moods.lowEnergyNegative.map((mood) => (
                            <button
                                key={mood.id}
                                className={`h-fit w-fit p-2 ${getColor(mood.type)} ${selectedMoods.includes(mood) ? 'opacity-55' : '' } rounded-md m-1 text-sm font-semibold cursor-pointer`}
                                onClick={() => handleMoodSelection(mood)}
                            >
                                {mood.name}
                            </button>
                        ))}

                        <h1 className="font-semibold m-2">High Energy Negative</h1>
                        {moods && moods.highEnergyNegative.map((mood) => (
                            <button
                                key={mood.id}
                                className={`h-fit w-fit p-2 ${getColor(mood.type)} ${selectedMoods.includes(mood) ? 'opacity-55' : '' } rounded-md m-1 text-sm font-semibold cursor-pointer`}
                                onClick={() => handleMoodSelection(mood)}
                            >
                                {mood.name}
                            </button>
                        ))}

                        <h1 className="font-semibold m-2">High Energy Positive</h1>
                        {moods && moods.highEnergyPositive.map((mood) => (
                            <button
                                key={mood.id}
                                className={`h-fit w-fit p-2 ${getColor(mood.type)} ${selectedMoods.includes(mood) ? 'opacity-55' : '' } rounded-md m-1 text-sm font-semibold cursor-pointer`}
                                onClick={() => handleMoodSelection(mood)}
                            >
                                {mood.name}
                            </button>
                        ))}
                    </div>
                    <div className="flex flex-col w-1/3">
                        <h1 className="font-semibold">Notes</h1>
                        <textarea
                            value={notes}
                            onChange={(e) => setNotes(e.target.value)}
                            className="w-full h-full p-2 shadow-lg rounded-md overflow-y-scroll scrollbar-none"
                        />
                    </div>
                    </div>
                </div>                
            </div>
            <div className="flex flex-row mt-2">
                <button 
                    onClick={handleSave}
                    className="h-fit w-fit bg-green-200 rounded-md p-1 mx-2 font-semibold"
                >
                    Save
                </button>
                <button
                    onClick={() => setShowOverlay(false)} 
                    className="h-fit w-fit bg-red-200 rounded-md p-1 mx-2 font-semibold"
                >
                    Close
                </button>
            </div>
        </div>
    );
};

export default CheckIn;