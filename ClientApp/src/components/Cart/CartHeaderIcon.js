import { NavLink } from 'reactstrap';
import { NavLink as RRNavLink} from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { cartHeaderIconAnimationClassesSelector, doneAnimating,  } from '../../state/slices/CartHeaderIconSlice';

import './CartHeaderIcon.css';


export const CartHeaderIcon = () => {
    const animationClasses = useSelector(cartHeaderIconAnimationClassesSelector);
    const dispatch = useDispatch();

    return (
        <NavLink tag={RRNavLink} to="/cart" className={`sidebar-toggler flex-shrink-0 ms-2 my-auto ${animationClasses}`} 
            onAnimationEnd={() => {
                dispatch(doneAnimating()); 
            }}>
            <i className={`fa fa-shopping-cart icon`}/>
        </NavLink>
    );
}