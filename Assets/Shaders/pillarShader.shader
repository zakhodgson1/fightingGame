Shader "GLSL/waterColor" { // defines the name of the shader 
    SubShader{ // Unity chooses the subshader that fits the GPU best
       Pass { // some shaders require multiple passes
          GLSLPROGRAM // here begins the part in Unity's GLSL

          #ifdef VERTEX // here begins the vertex shader

          void main() // all vertex shaders define a main() function
          {
             gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
             // this line transforms the predefined attribute 
             // gl_Vertex of type vec4 with the predefined
             // uniform gl_ModelViewProjectionMatrix of type mat4
             // and stores the result in the predefined output 
             // variable gl_Position of type vec4.
          }

       #endif // here ends the definition of the vertex shader

       #ifdef FRAGMENT // here begins the fragment shader

        uniform vec2 u_resolution;
        uniform float u_time;
        const int AMOUNT = 10;

        void main() {
            vec2 coord = 20.0 * (gl_FragCoord.xy - u_resolution / 2.0) / min(u_resolution.y, u_resolution.x);

            float len;

            for (int i = 0; i < AMOUNT; i++)
            {
                len = length(vec2(coord.x, coord.y));

                coord.x = coord.x + cos(coord.y + sin(len)) + cos(u_time / 20.0);
                coord.y = coord.y - sin(coord.x + cos(len)) - sin(u_time / 5.0);
            }

      gl_FragColor = vec4(cos(len * u_time), cos(len), cos(len * u_time), 1.0);
  }

  #endif // here ends the definition of the fragment shader

  ENDGLSL // here ends the part in GLSL 
  }