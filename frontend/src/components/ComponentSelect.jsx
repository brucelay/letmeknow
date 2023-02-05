import { createSignal } from "solid-js";

export default function ComponentSelect(props) {
  const addFunction = (func) => {
    console.log(func);
    const [options, setOptions] = createSignal({});
    props.setFunctions([...props.functions, { ...func}]);
  };

  return (
    <>
      <For each={props.modules()}>
        {(module) => (
          <h1 onClick={() => addFunction(module)}>{module.function}</h1>
        )}
      </For>
    </>
  );
}
