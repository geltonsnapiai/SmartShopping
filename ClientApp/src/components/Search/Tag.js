import './Tag.css';

export const Tag = (props) => {
    return (
        <label className={`tag tag-${props.type} me-2 px-2`}>
            {props.name}
        </label>
    );
}