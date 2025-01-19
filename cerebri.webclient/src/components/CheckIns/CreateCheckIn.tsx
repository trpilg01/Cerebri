import ErrorPopUp from "components/popups/ErrorPopUp";
import MoodMenu from "components/moodmenu/MoodMenu";
import { CreateCheckInDTO, Mood } from "data/dataTypes";
import { useCheckIns, useMoods } from "hooks";
import { useState } from "react";

interface CheckInProps {
    setShowOverlay: (value: boolean) => void;
}

const CreateCheckIn:React.FC<CheckInProps> = ({ setShowOverlay }) => {
    const [selectedMoods, setSelectedMoods] = useState<Mood[]>([]);
    const [notes, setNotes] = useState<string>('');
    const { createCheckIn, error, setError } = useCheckIns();
    const { moods } = useMoods();
    
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
        if (selectedMoods.length === 0) {
            setError("At least one mood must be selected");
            return;
        }
        if (notes.length > 1000) {
            setError("Note must be less than 1000 characters");
            return;
        }

        const request: CreateCheckInDTO = {
            content: notes,
            moods: selectedMoods
        }
        await createCheckIn(request);
    };

    return (
        <div className="fixed flex flex-col inset-0 bg-black bg-opacity-50 items-center justify-center z-50">
            <div className="flex w-3/5 h-[90%] bg-astra p-10 rounded-md">
            <div className="flex flex-col h-full w-full">
                <div className="flex flex-row h-full w-full gap-2">
                    {/* Mood Menu */}
                    <MoodMenu
                        moods={moods}
                        selectedMoods={selectedMoods}
                        onSelect={handleMoodSelection}
                        width="w-2/3"
                    />
                    <div className="flex flex-col w-1/3 text-lg text-center">
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
            {error && <ErrorPopUp message={error} setError={setError} />}
        </div>
    );
};

export default CreateCheckIn;