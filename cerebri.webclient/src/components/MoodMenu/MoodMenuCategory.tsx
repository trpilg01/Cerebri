import { Mood } from "data/dataTypes";
import { getColor } from "services";

interface MoodMenuCategoryProps {
    moods: Mood[] | undefined;
    title: string;
    selectedMoods: Mood[]
    onSelect: (mood: Mood) => void;
};

const MoodMenuCategory:React.FC<MoodMenuCategoryProps> = ({ moods, title, selectedMoods, onSelect }) => (
    <>
        <h1 className="font-bold my-2">{title}</h1>
        <div className="flex flex-row flex-wrap mx-2 gap-2 justify-center">
            {moods?.map((mood) => (
                <button
                    className={`p-1 font-semibold ${getColor(mood.type)} rounded-md ${selectedMoods.includes(mood) ? "opacity-50" : ""}`}
                    onClick={() => onSelect(mood)}
                >
                    {mood.name}
                </button>
            ))}
        </div>
    </>
)


export default MoodMenuCategory;