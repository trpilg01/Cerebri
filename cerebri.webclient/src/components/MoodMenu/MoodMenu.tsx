import { Mood, Moods } from "data/dataTypes";
import MoodMenuCategory from "./MoodMenuCategory";

interface MoodMenuProps {
    moods: Moods | null;
    selectedMoods: Mood[];
    onSelect: (mood: Mood) => void;
    width: string;
};

const MoodMenu:React.FC<MoodMenuProps> = ({ moods, selectedMoods, onSelect, width }) => (
    <div className={`flex flex-col h-full ${width} text-center items-center`}>
        <h1 className="font-semibold text-lg">Moods</h1>
        <div className={`flex flex-col h-full w-full rounded-md bg-blizzardBlue text-center items-center overflow-scroll scrollbar-none p-2`}>
            
            <h1 className="font-bold">Selected Moods:</h1>
            <div className="flex flex-row">
                {selectedMoods.length === 0 ? <h1 className="font-semibold text-lg">No moods Selected</h1> : selectedMoods.map((mood) => (
                            <a className="m-1 text-lg font-semibold">{mood.name}</a>
                ))}
            </div>

            <MoodMenuCategory
                moods={moods?.lowEnergyPositive}
                title="Low Energy Positive"
                onSelect={onSelect}
                selectedMoods={selectedMoods}
            />
            <MoodMenuCategory
                moods={moods?.lowEnergyNegative}
                title="Low Energy Negative"
                onSelect={onSelect}
                selectedMoods={selectedMoods}
            />
            <MoodMenuCategory
                moods={moods?.highEnergyPositive}
                title="High Energy Positive"
                onSelect={onSelect}
                selectedMoods={selectedMoods}
            />
            <MoodMenuCategory
                moods={moods?.highEnergyNegative}
                title="High Energy Negative"
                onSelect={onSelect}
                selectedMoods={selectedMoods}
            />  
        </div>
    </div>
);

export default MoodMenu;